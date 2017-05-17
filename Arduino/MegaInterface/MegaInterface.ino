/*
   File:      MegaInterface.ino
   Date:      16/05/2017
   Author:    What's The Geek
   Copyright: BlueDragon
*/

#include <EEPROM.h>

#define VERSION     "1.0.0.5"
#define BAUDRATE    115200

int pin = 0;
int mode = 0;
int state = 0;
int value = 0;
char value_l = 0;
char value_h = 0;
int baud = 0;
char baud_ll = 0;
char baud_lh = 0;
char baud_hl = 0;
String str = "";
char data = 0;
char addr_l = 0;
char addr_h = 0;
int addr = 0;

void setup() {
  Serial.begin(BAUDRATE);
}

void loop() {
  if (Serial.available()) {
    char c = Serial.read();
    switch (c) {
      case 0x00:  // getFirmwareVersion
        Serial.println(VERSION);
        break;
      case 0x01:  // pinMode
        while (!Serial.available());
        pin = (int)Serial.read();
        while (!Serial.available());
        mode = (int)Serial.read();
        pinMode(pin, mode);
        break;
      case 0x02:  // digitalWrite
        while (!Serial.available());
        pin = (int)Serial.read();
        while (!Serial.available());
        state = (int)Serial.read();
        digitalWrite(pin, state);
        break;
      case 0x03:  // digitalRead
        while (!Serial.available());
        pin = (int)Serial.read();
        Serial.write(digitalRead(pin));
        break;
      case 0x04:  // analogWrite
        while (!Serial.available());
        pin = (int)Serial.read();
        while (!Serial.available());
        state = (int)Serial.read();
        analogWrite(pin, state);
        break;
      case 0x05:  // analogRead
        while (!Serial.available());
        pin = (int)Serial.read();
        value = analogRead(pin);
        value_l = value & 0xFF;
        value_h = value >> 8;
        Serial.write(value_l);
        Serial.write(value_h);
        break;
      case 0x06:  // Serial1_Begin
        while (!Serial.available());
        baud_ll = Serial.read();
        while (!Serial.available());
        baud_lh = Serial.read();
        while (!Serial.available());
        baud_hl = Serial.read();
        baud = 0;
        baud = (baud_hl << 16);
        baud = baud | (baud_lh << 8);
        baud = baud | (baud_ll);
        Serial1.begin(baud);
        Serial.write(0xE0);
        break;
      case 0x07:  // Serial2_Begin
        while (!Serial.available());
        baud_ll = Serial.read();
        while (!Serial.available());
        baud_lh = Serial.read();
        while (!Serial.available());
        baud_hl = Serial.read();
        baud = 0;
        baud = (baud_hl << 16);
        baud = baud | (baud_lh << 8);
        baud = baud | (baud_ll);
        Serial2.begin(baud);
        Serial.write(0xE0);
        break;
      case 0x08:  // Serial3_Begin()
        while (!Serial.available());
        baud_ll = Serial.read();
        while (!Serial.available());
        baud_lh = Serial.read();
        while (!Serial.available());
        baud_hl = Serial.read();
        baud = 0;
        baud = (baud_hl << 16);
        baud = baud | (baud_lh << 8);
        baud = baud | (baud_ll);
        Serial3.begin(baud);
        Serial.write(0xE0);
        break;
      case 0x09:  // Serial1_Print
        str = "";
        while (!Serial.available());
        while (Serial.available()) {
          str = str + (char)Serial.read();
        }
        Serial1.print(str);
        break;
      case 0x0A:  // Serial1_Read
        Serial.write(Serial1.read());
        break;
      case 0x0B:  // Serial2_Print
        str = "";
        while (!Serial.available());
        while (Serial.available()) {
          str = str + (char)Serial.read();
        }
        Serial2.print(str);
        break;
      case 0x0C:  // Serial2_Read
        Serial.write(Serial2.read());
        break;
      case 0x0D:  // Serial3_Print
        str = "";
        while (!Serial.available());
        while (Serial.available()) {
          str = str + (char)Serial.read();
        }
        Serial3.print(str);
        break;
      case 0x0E:  // Serial3_Read
        Serial.write(Serial3.read());
        break;
      case 0x0F:  // Serial1_Write
        while (!Serial.available());
        Serial1.write(Serial.read());
        break;
      case 0x10:  // Serial2_Write
        while (!Serial.available());
        Serial2.write(Serial.read());
        break;
      case 0x11:  // Serial3_Write
        while (!Serial.available());
        Serial3.write(Serial.read());
        break;
      case 0x12:  // Serial1_End
        Serial1.end();
        break;
      case 0x13:  // Serial2_End
        Serial2.end();
        break;
      case 0x14:  // Serial3_End
        Serial3.end();
        break;
      case 0x15:  // EEPROM_Write
        while (!Serial.available());
        addr_l = Serial.read();
        while (!Serial.available());
        addr_h = Serial.read();
        while (!Serial.available());
        data = Serial.read();
        addr = 0;
        addr = addr | (addr_h << 8);
        addr = addr | (addr_l);
        EEPROM.write(addr, data);
        break;
      case 0x16:  // EEPROM_Read
        while (!Serial.available());
        addr_l = Serial.read();
        while (!Serial.available());
        addr_h = Serial.read();
        addr = 0;
        addr = addr | (addr_h << 8);
        addr = addr | (addr_l);
        Serial.write(EEPROM.read(addr));
        break;
    }
  }
}

/* ---------------------------------- DEBUGGING PURPOSES ---------------------------------- */
void blink() {
  pinMode(13, OUTPUT);
  digitalWrite(13, 1);
  delay(1000);
  digitalWrite(13, 0);
  delay(1000);
  digitalWrite(13, 1);
  delay(1000);
  digitalWrite(13, 0);
  delay(1000);
  digitalWrite(13, 1);
  delay(1000);
  digitalWrite(13, 0);
  delay(1000);
}
/* ---------------------------------- DEBUGGING PURPOSES ---------------------------------- */

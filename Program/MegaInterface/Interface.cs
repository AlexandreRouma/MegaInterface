using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaInterface
{
    public class Interface
    {

        public enum PinMode
        {
            INPUT=(int)0,
            OUTPUT=(int)1,
        }

        public enum AnalogPin
        {
            A0 = (int)0,
            A1 = (int)1,
            A2 = (int)2,
            A3 = (int)3,
            A4 = (int)4,
            A5 = (int)5,
            A6 = (int)6,
            A7 = (int)7,
            A8 = (int)8,
            A9 = (int)9,
            A10 = (int)10,
            A11 = (int)11,
            A12 = (int)12,
            A13 = (int)13,
            A14 = (int)14,
            A15 = (int)15,
        }

        public enum State
        {
            LOW = (int)0,
            HIGH = (int)1,
        }

        private SerialPort serial = new SerialPort();
        public readonly String SerialPort = "COM1";
        public readonly long Baudrate = 9600;

        /// <summary>
        /// Creates an instance of Interface
        /// </summary>
        /// <param name="_SerialPort">The COM port of the arduino</param>
        /// <param name="_Baudrate">The baudrate used to communicate with the arduino</param>
        public Interface(String _SerialPort, int _Baudrate)
        {
            SerialPort = _SerialPort;
            Baudrate = _Baudrate;
            serial.PortName = _SerialPort;
            serial.BaudRate = _Baudrate;
            serial.ReadTimeout = 1000;
        }

        /// <summary>
        /// Opens the connection between the computer and the arduino
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                serial.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes the connection between the computer and the arduino
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                serial.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the serial port is open
        /// </summary>
        /// <returns>True if the port is open, false if not</returns>
        public bool isOpen()
        {
            return serial.IsOpen;
        }

        /// <summary>
        /// Gets the firmware version of the arduino
        /// </summary>
        /// <returns>Firmware version</returns>
        public String getFirmwareVersion()
        {
            try
            {
                serial.Write("" + (char)0x00);
                return serial.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Sets the pinmode of a pin
        /// </summary>
        /// <param name="pin">Arduino pin</param>
        /// <param name="mode">Mode</param>
        /// <returns>Succeded</returns>
        public bool pinMode(int pin, PinMode mode)
        {
            try
            {
                serial.Write("" + (char)0x01 + (char)pin + (char)mode);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the state of a pin
        /// </summary>
        /// <param name="pin">Arduino pin</param>
        /// <param name="value">State</param>
        /// <returns>Succeded</returns>
        public bool digitalWrite(int pin, State value)
        {
            try
            {
                serial.Write("" + (char)0x02 + (char)pin + (char)value);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// Reads the state of a pin
        /// </summary>
        /// <param name="pin">Arduino pin</param>
        /// <returns>State</returns>
        public int digitalRead(int pin)
        {
            try
            {
                serial.Write("" + (char)0x03 + (char)pin);
                return serial.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Sets the PWM value of a pin
        /// </summary>
        /// <param name="pin">Arduino pin</param>
        /// <param name="value">PWM Value (0 -> 255)</param>
        /// <returns>Succeded</returns>
        public bool analogWrite(int pin, int value)
        {
            try
            {
                serial.Write("" + (char)0x04 + (char)pin + (char)value);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Reads the value of an analog pin
        /// </summary>
        /// <param name="pin">Arduino analog pin</param>
        /// <returns>Value</returns>
        public int analogRead(AnalogPin pin)
        {
            try
            {
                serial.Write("" + (char)0x05 + (char)pin);
                byte value_l = (byte)serial.ReadByte();
                byte value_h = (byte)serial.ReadByte();
                return (value_h << 8) | value_l;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Opens the Serial1 UART
        /// </summary>
        /// <param name="baudrate">Serial1 baudrate</param>
        /// <returns>Succeded</returns>
        public bool Serial1_Begin(int baudrate)
        {
            try
            {
                byte baud_ll = (byte)(baudrate);
                byte baud_lh = (byte)(baudrate >> 4);
                byte baud_hl = (byte)(baudrate >> 12);
                serial.Write("" + (char)0x06 + (char)baud_ll + (char)baud_lh + (char)baud_hl);
                if (serial.ReadChar() == 0xE0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Opens the Serial2 UART
        /// </summary>
        /// <param name="baudrate">Serial2 baudrate</param>
        /// <returns>Succeded</returns>
        public bool Serial2_Begin(int baudrate)
        {
            try
            {
                byte baud_ll = (byte)(baudrate);
                byte baud_lh = (byte)(baudrate >> 4);
                byte baud_hl = (byte)(baudrate >> 12);
                serial.Write("" + (char)0x07 + (char)baud_ll + (char)baud_lh + (char)baud_hl);
                if (serial.ReadChar() == 0xE0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Opens the Serial3 UART
        /// </summary>
        /// <param name="baudrate">Serial3 baudrate</param>
        /// <returns></returns>
        public bool Serial3_Begin(int baudrate)
        {
            try
            {
                byte baud_ll = (byte)(baudrate);
                byte baud_lh = (byte)(baudrate >> 4);
                byte baud_hl = (byte)(baudrate >> 12);
                serial.Write("" + (char)0x08 + (char)baud_ll + (char)baud_lh + (char)baud_hl);
                if (serial.ReadChar() == 0xE0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prints a string to Serial1
        /// </summary>
        /// <param name="str">String to be printed</param>
        /// <returns>Succeded</returns>
        public bool Serial1_Print(String str)
        {
            try
            {
                serial.Write("" + (char)0x09 + str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prints a string along with a new line carater to Serial1
        /// </summary>
        /// <param name="str">String to be printed</param>
        /// <returns>Succeded</returns>
        public bool Serial1_Println(String str)
        {
            try
            {
                serial.Write("" + (char)0x09 + str + "\n");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a character from Serial1
        /// </summary>
        /// <returns>Character</returns>
        public int Serial1_Read()
        {
            try
            {
                serial.Write("" + (char)0x0A);
                return serial.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writes a character to Serial1
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Succeded</returns>
        public bool Serial1_Write(char c)
        {
            try
            {
                serial.Write("" + (char)0x0B + c);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prints a string to Serial2
        /// </summary>
        /// <param name="str">String to be printed</param>
        /// <returns>Succeded</returns>
        public bool Serial2_Print(String str)
        {
            try
            {
                serial.Write("" + (char)0x0C + str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prints a string along with a new line carater to Serial2
        /// </summary>
        /// <param name="str">String to be printed</param>
        /// <returns>Succeded</returns>
        public bool Serial2_Println(String str)
        {
            try
            {
                serial.Write("" + (char)0x0C + str + "\n");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a character from Serial2
        /// </summary>
        /// <returns>Character</returns>
        public int Serial2_Read()
        {
            try
            {
                serial.Write("" + (char)0x0D);
                return serial.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writes a character to Serial2
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Succeded</returns>
        public bool Serial2_Write(char c)
        {
            try
            {
                serial.Write("" + (char)0x0E + c);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prints a string to Serial3
        /// </summary>
        /// <param name="str">String to be printed</param>
        /// <returns>Succeded</returns>
        public bool Serial3_Print(String str)
        {
            try
            {
                serial.Write("" + (char)0x0F + str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prints a string along with a new line carater to Serial3
        /// </summary>
        /// <param name="str">String to be printed</param>
        /// <returns>Succeded</returns>
        public bool Serial3_Println(String str)
        {
            try
            {
                serial.Write("" + (char)0x0F + str + "\n");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a character from Serial3
        /// </summary>
        /// <returns>Character</returns>
        public int Serial3_Read()
        {
            try
            {
                serial.Write("" + (char)0x10);
                return serial.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writes a character to Serial3
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Succeded</returns>
        public bool Serial3_Write(char c)
        {
            try
            {
                serial.Write("" + (char)0x11 + c);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes Serial1
        /// </summary>
        /// <returns>Succeded</returns>
        public bool Serial1_End()
        {
            try
            {
                serial.Write("" + (char)0x12);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes Serial2
        /// </summary>
        /// <returns>Succeded</returns>
        public bool Serial2_End()
        {
            try
            {
                serial.Write("" + (char)0x12);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes Serial3
        /// </summary>
        /// <returns>Succeded</returns>
        public bool Serial3_End()
        {
            try
            {
                serial.Write("" + (char)0x12);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Writes a value to an address in the EEPROM
        /// </summary>
        /// <param name="addr">The address to write to</param>
        /// <param name="data">The data to write at the address</param>
        /// <returns>Succeded</returns>
        public bool EEPROM_Write(int addr, char data)
        {
            try
            {
                byte addr_l = (byte)(addr);
                byte addr_h = (byte)(addr >> 8);
                serial.Write("" + (char)0x15 + (char)addr_l + (char)addr_h + (char)data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads a value from EEPROM
        /// </summary>
        /// <param name="addr">The address to read the value from</param>
        /// <returns>Value</returns>
        public int EEPROM_Read(int addr)
        {
            try
            {
                byte addr_l = (byte)(addr);
                byte addr_h = (byte)(addr >> 8);
                serial.Write("" + (char)0x16 + (char)addr_l + (char)addr_h);
                return serial.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

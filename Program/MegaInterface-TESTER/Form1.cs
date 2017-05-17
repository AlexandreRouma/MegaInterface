using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MegaInterface;
using System.IO.Ports;

namespace MegaInterface_TESTER
{
    public partial class Form1 : Form
    {

        Interface arduino = new Interface("COM1",115200);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            firm_ver_lbl.Text = "";
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < SerialPort.GetPortNames().Count(); i++)
            {
                comboBox1.Items.Add(SerialPort.GetPortNames()[i]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (arduino.isOpen()) 
                {
                    arduino.Close();
                }
                arduino = new Interface(comboBox1.Text, int.Parse(numericUpDown1.Value.ToString()));
                arduino.Open();
                firm_ver_lbl.Text = arduino.getFirmwareVersion();
                info_box.Enabled = true;
                pinmode_box.Enabled = true;
                digitalwrite_box.Enabled = true;
                digitalread_box.Enabled = true;
                analogwrite_box.Enabled = true;
                analogread_box.Enabled = true;
                sr1_box.Enabled = true;
                sr2_box.Enabled = true;
                sr3_box.Enabled = true;
                eep_box.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, could not connect to arduino !\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Interface.PinMode mode = Interface.PinMode.INPUT;
            if (comboBox2.SelectedIndex == 1)
            {
                mode = Interface.PinMode.OUTPUT;
            }
            arduino.pinMode(int.Parse(numericUpDown2.Value.ToString()), mode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Interface.State state = Interface.State.LOW;
            if (comboBox3.SelectedIndex == 1)
            {
                state = Interface.State.HIGH;
            }
            arduino.digitalWrite(int.Parse(numericUpDown3.Value.ToString()), state);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (arduino.digitalRead(int.Parse(numericUpDown3.Value.ToString())) == 1)
            {
                textBox1.Text = "HIGH";
            }
            else
            {
                textBox1.Text = "LOW";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            arduino.analogWrite(int.Parse(numericUpDown6.Value.ToString()), int.Parse(numericUpDown5.Value.ToString()));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "" + arduino.analogRead((Interface.AnalogPin)int.Parse(numericUpDown7.Value.ToString()));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            arduino.Serial1_Begin(int.Parse(numericUpDown8.Value.ToString()));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            arduino.Serial1_End();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = "0x" + arduino.Serial1_Read().ToString("X");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            arduino.Serial1_Print(textBox3.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            arduino.Serial2_Begin(int.Parse(numericUpDown9.Value.ToString()));
        }

        private void button12_Click(object sender, EventArgs e)
        {
            arduino.Serial2_End();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox6.Text = "0x" + arduino.Serial2_Read().ToString("X");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            arduino.Serial2_Print(textBox6.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            arduino.Serial3_Begin(int.Parse(numericUpDown10.Value.ToString()));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            arduino.Serial3_End();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox8.Text = "0x" + arduino.Serial3_Read().ToString("X");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            arduino.Serial3_Print(textBox8.Text);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            numericUpDown12.Value = arduino.EEPROM_Read(int.Parse(numericUpDown11.Value.ToString()));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            arduino.EEPROM_Write(int.Parse(numericUpDown11.Value.ToString()), (char)int.Parse(numericUpDown12.Value.ToString()));
        }


    }
}

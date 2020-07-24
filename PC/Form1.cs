using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino_Serial_Pc
{
    public partial class Form1 : Form
    {
        //object serialport to listen usb
        System.IO.Ports.SerialPort Port;

        //variable to check if arduino is connect
        bool IsClosed = false;

        bool state1 = false;
        bool state2 = false;

        bool lastBtn1 = false;
        bool lastBtn2 = false;
        bool resetBtn = false;

        public Form1()
        {
            InitializeComponent();

            //configuration of arduino, enter correct serial parameter
            Port = new System.IO.Ports.SerialPort();
            Port.PortName = "COM3";
            Port.BaudRate = 9600;
            Port.ReadTimeout = 500;

            try
            {
                Port.Open();
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //A Thread to listen forever the serial port
            Thread Hilo = new Thread(ListenSerial);
            Hilo.Start();

            textBox5.Invoke(new MethodInvoker(
                delegate
                {
                    textBox5.BackColor = Color.Red;
                    textBox5.Text = "OFF";
                }
                ));
            textBox6.Invoke(new MethodInvoker(
                delegate
                {
                    textBox6.BackColor = Color.Red;
                    textBox6.Text = "OFF";
                }
                ));
        }


        private void ListenSerial()
        {
            while (!IsClosed)
            {
                try
                {
                    //read to data from arduino
                    string AString = Port.ReadLine();
                    AString = AString.TrimEnd('\r');

                    if (AString == "BTN1")
                    {
                        string temp = Port.ReadLine();
                        temp = temp.TrimEnd('\r');

                        if (temp != lastBtn1.ToString() && temp == "True")
                        {
                            state1 = !state1;
                            //write the data in something textbox
                            textBox5.Invoke(new MethodInvoker(
                                delegate
                                {
                                    if (state1)
                                    {
                                        textBox5.BackColor = Color.Green;
                                        textBox5.Text = "ON";
                                    }
                                    else
                                    {
                                        textBox5.BackColor = Color.Red;
                                        textBox5.Text = "OFF";
                                    }
                                }
                                ));
                        }
                        if (temp == "True")
                        {
                            lastBtn1 = true;
                        }
                        else
                        {
                            lastBtn1 = false;
                        }
                    }
                    else if (AString == "BTN2")
                    {
                        string temp = Port.ReadLine();
                        temp = temp.TrimEnd('\r');

                        if (temp != lastBtn2.ToString() && temp == "True")
                        {
                            state2 = !state2;
                            //write the data in something textbox
                            textBox6.Invoke(new MethodInvoker(
                                delegate
                                {
                                    if (state2)
                                    {
                                        textBox6.BackColor = Color.Green;
                                        textBox6.Text = "ON";
                                    }
                                    else
                                    {
                                        textBox6.BackColor = Color.Red;
                                        textBox6.Text = "OFF";
                                    }
                                }
                                ));
                        }
                        if (temp == "True")
                        {
                            lastBtn2 = true;
                        }
                        else
                        {
                            lastBtn2 = false;
                        }
                    }

                    else if (AString == "BTN3")
                    {
                        string temp = Port.ReadLine();
                        temp = temp.TrimEnd('\r');

                        if (temp != resetBtn.ToString() && temp == "True")
                        {
                            state1 = false;
                            state2 = false;
                            textBox5.Invoke(new MethodInvoker(
                                delegate
                                {
                                    if (state1)
                                    {
                                        textBox5.BackColor = Color.Green;
                                        textBox5.Text = "ON";
                                    }
                                    else
                                    {
                                        textBox5.BackColor = Color.Red;
                                        textBox5.Text = "OFF";
                                    }
                                }
                                ));
                            textBox6.Invoke(new MethodInvoker(
                                delegate
                                {
                                    if (state2)
                                    {
                                        textBox6.BackColor = Color.Green;
                                        textBox6.Text = "ON";
                                    }
                                    else
                                    {
                                        textBox6.BackColor = Color.Red;
                                        textBox6.Text = "OFF";
                                    }
                                }
                                ));
                        }
                        if (temp == "True")
                        {
                            resetBtn = true;
                        }
                        else
                        {
                            resetBtn = false;
                        }
                    }
                    
                    else
                    {
                        lastBtn1 = false;
                        lastBtn2 = false;
                        resetBtn = false;
                    }

                }
                catch { }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when the form will be closed this line close the serial port
            IsClosed = true;
            if (Port.IsOpen)
                Port.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


    }
}


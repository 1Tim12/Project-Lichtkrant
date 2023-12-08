using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Lichtkrant
{
    public class Snelheid
    {
        public bool Speed1 { get; set; }
        public bool Speed2 { get; set; }
        public bool Speed3 { get; set; }

        public void WriteToSerialPort(string text, System.IO.Ports.SerialPort _serialPort)
        {
            if (Speed1)
            {
                _serialPort.Write("<ID01><PA><FS><FZ>" + " " + text + " " + Convert.ToChar(13) + Convert.ToChar(10));
                _serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
            }
            else if (Speed2)
            {
                _serialPort.Write("<ID01><PA><FS><FY>" + " " + text + " " + Convert.ToChar(13) + Convert.ToChar(10));
                _serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
            }
            else if (Speed3)
            {
                _serialPort.Write("<ID01><PA><FS><FX>" + " " + text + " " + Convert.ToChar(13) + Convert.ToChar(10));
                _serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
            }
            else
            {
                MessageBox.Show("Geen snelheid gekozen");
            }
        }
    }
}
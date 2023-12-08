using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Lichtkrant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Checkboxes checkboxes;

        SerialPort _serialPort;
        const int NUMBER_OF_DMX_BYTES = 513;
        byte[] _data;

        int Snelheid = 0;

        public MainWindow()
        {
            InitializeComponent();

            compoorten.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                compoorten.Items.Add(s);

            checkboxes = new Checkboxes();

            _serialPort = new SerialPort();
        }

        private void checkTraag_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxes.Speed1 == true)
            {
                Snelheid = 1;

                checkNormaal.IsChecked = false;
                checkNormaal.IsChecked = false;
            }
        }
        private void checkNormaal_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxes.Speed2 == true)
            {
                Snelheid = 2;

                checkSnel.IsChecked = false;
                checkTraag.IsChecked = false;
            }
        }

        private void checkSnel_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxes.Speed3 == true)
            {
                Snelheid = 3;

                checkNormaal.IsChecked = false;
                checkTraag.IsChecked = false;
            }
        }

        private void compoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                if (compoorten.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = compoorten.SelectedItem.ToString();
                    _serialPort.Open();

                    tbxTekst.IsEnabled = true;
                    checkTraag.IsEnabled = true;
                    checkNormaal.IsEnabled = true;
                    checkSnel.IsEnabled = true;
                }
                else
                {
                    tbxTekst.IsEnabled = false;
                    checkTraag.IsEnabled = false;
                    checkNormaal.IsEnabled = false;
                    checkSnel.IsEnabled = false;

                    MessageBox.Show("Kies een compoort!");
                }
            }
        }

        private void SendData(byte[] data, SerialPort serialPort)
        {
            
        }

        private void _SendData(object? sender, EventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((_serialPort != null) && _serialPort.IsOpen)
            {
                _serialPort.Write(new byte[] { 0 }, 0, 1);
                _serialPort.Dispose();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {

            if (checkboxes.Speed1 == true)
            {
                _serialPort.Write("<ID01><PA><FS><FZ>" + " " + tbxTekst.Text + " " + Convert.ToChar(13) + Convert.ToChar(10));
                _serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
            }

            else if (Snelheid == 2)
            {
                _serialPort.Write("<ID01><PA><FS><FY>" + " " + tbxTekst.Text + " " + Convert.ToChar(13) + Convert.ToChar(10));
                _serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
            }

            else if (Snelheid == 3)
            {
                _serialPort.Write("<ID01><PA><FZ><FX>" + " " + tbxTekst.Text + " " + Convert.ToChar(13) + Convert.ToChar(10));
                _serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
            }

            else
            {
                MessageBox.Show("Er is een probleem");
            }

            //_serialPort.Write("<ID01><PA><FS><FX>" + " " + tbxTekst.Text + " " + Convert.ToChar(13) + Convert.ToChar(10));
            //_serialPort.Write("<ID01><RPA>" + Convert.ToChar(13) + Convert.ToChar(10));
        }
    }
}

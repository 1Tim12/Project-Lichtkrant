using System;
using System.Collections.Generic;
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
            }
        }
        private void checkNormaal_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxes.Speed2 == true)
            {
                Snelheid = 2;
            }
        }

        private void checkSnel_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxes.Speed3 == true)
            {
                Snelheid = 3;
            }
        }

        private void compoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
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

                    MessageBox.Show($"Kies een compoort!", "Fout",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void SendData(byte[] data, SerialPort serialPort)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                { 
                    serialPort.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij verzenden van de data: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((_serialPort != null) && _serialPort.IsOpen)
            {
                _serialPort.Write(new byte[] { 0 }, 0, 1);
                _serialPort.Dispose();
            }
        }

        private void tbxTekst_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tekst = tbxTekst.Text;

           byte _tekst[] = Convert.ToByte(tekst);
            //SendData(Encoding.ASCII.GetBytes(tekst), _serialPort);
            SendData(_tekst, _serialPort);
        }
    }
}

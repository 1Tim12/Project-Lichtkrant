using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
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

namespace tekst_versturen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _serialPort;

        public MainWindow()
        {
            InitializeComponent();
            _serialPort = new SerialPort();

            compoorten.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                compoorten.Items.Add(s);
        }

        private void btnVerstuurTekst_Click(object sender, RoutedEventArgs e)
        {
            string tekst = tbxTekst.Text;
            SendData(Encoding.ASCII.GetBytes(tekst), _serialPort);
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
                MessageBox.Show($"Fout bij het verzenden van gegevens: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void compoorten_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
                }
                else
                {
                    tbxTekst.IsEnabled = false;

                    MessageBox.Show("Kies een compoort!");
                }
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
    }
}
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

        public MainWindow()
        {
            InitializeComponent();

            compoorten.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                compoorten.Items.Add(s);

            checkboxes = new Checkboxes();

            _serialPort = new SerialPort();
        }

        public void checkTraag_Checked(object sender, RoutedEventArgs e)
        {

        }
        public void checkNormaal_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        public void checkSnel_Checked(object sender, RoutedEventArgs e)
        {
            
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
                    Send.IsEnabled = true;
                }
                else
                {
                    tbxTekst.IsEnabled = false;
                    checkTraag.IsEnabled = false;
                    checkNormaal.IsEnabled = false;
                    checkSnel.IsEnabled = false;
                    Send.IsEnabled = false;

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

        public void Send_Click(object sender, RoutedEventArgs e)
        {
            checkboxes.Speed1 = checkTraag.IsChecked.Value;
            checkboxes.Speed2 = checkNormaal.IsChecked.Value;
            checkboxes.Speed3 = checkSnel.IsChecked.Value;

            // Haal de tekst op uit tbxTekst
            string text = tbxTekst.Text;

            // Schrijf naar de seriële poort
            checkboxes.WriteToSerialPort(text, _serialPort);
        }
    }
}

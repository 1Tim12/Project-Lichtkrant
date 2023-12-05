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
        SerialPort _serialPort;
        const int NUMBER_OF_DMX_BYTES = 513;
        byte[] _data;
        public MainWindow()
        {
            InitializeComponent();

            compoorten.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                compoorten.Items.Add(s);

        }

        private void checkTraag_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void checkNormaal_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void checkSnel_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void SendData(byte[] data, SerialPort serialPort)
        {
            

            
        }

        private void _SendData(object? sender, EventArgs e)
        {
            
        }
    }
}

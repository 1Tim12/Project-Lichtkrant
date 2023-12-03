using System;
using System.Collections.Generic;
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

namespace Project_Lichtkrant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void checkTraag_Checked(object sender, RoutedEventArgs e)
        {
            checkSnel.IsChecked = true;
            checkNormaal.IsChecked = false;
            checkTraag.IsChecked = false;
        }

        private void checkNormaal_Checked(object sender, RoutedEventArgs e)
        {
            checkSnel.IsChecked = false;
            checkNormaal.IsChecked = true;
            checkTraag.IsChecked = false;
        }

        private void checkSnel_Checked(object sender, RoutedEventArgs e)
        {
            checkSnel.IsChecked = false;
            checkNormaal.IsChecked = false;
            checkTraag.IsChecked = true;
        }
    }
}

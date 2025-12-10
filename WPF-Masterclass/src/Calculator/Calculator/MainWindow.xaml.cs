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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            resultLabel.Content = "0";
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            Button numberButton = (Button) sender;

            string numero = numberButton.Content.ToString();

            if(resultLabel.Content.ToString() == "0") 
            {

                resultLabel.Content = $"{numero}";
            }
            else 
            {
                resultLabel.Content += $"{numero}";
            }
        }

        private void OpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void eqButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

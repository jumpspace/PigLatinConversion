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

namespace PigLatinConversion
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

        private void ConvertButton_OnClick(object sender, RoutedEventArgs e)
        {
            string currentText;

            StringBuilder convertedText = new StringBuilder(PigLatin.Convert(InputTextBox.Text));
            if (ConvertedTextBox.Text != String.Empty)
            {
                currentText = ConvertedTextBox.Text;
                ConvertedTextBox.Text = currentText + convertedText.ToString() + "\r\n" ;
            }
            else
            {
                ConvertedTextBox.Text = convertedText.ToString() + "\r\n";
            }
            
            InputTextBox.Text = String.Empty;
        }
    }
}

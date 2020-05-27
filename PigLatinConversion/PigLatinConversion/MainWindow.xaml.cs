using System;
using System.Text;
using System.Windows;

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
            if (InputCheck.InputValid(InputTextBox.Text))
            {
                string currentText;

                StringBuilder convertedText = new StringBuilder(PigLatin.Convert(InputTextBox.Text));
                if (ConvertedTextBox.Text != String.Empty)
                {
                    currentText = ConvertedTextBox.Text;
                    ConvertedTextBox.Text = currentText + convertedText.ToString() + "\r\n";
                }
                else
                {
                    ConvertedTextBox.Text = convertedText.ToString() + "\r\n";
                }

                InputTextBox.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Invalid input. Only letters and spaces allowed.", "Pig Latin Conversion", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                InputTextBox.Text = String.Empty;
            }
        }
    }
}

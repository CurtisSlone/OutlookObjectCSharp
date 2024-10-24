using System;
using System.Windows;

namespace Outlook
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the input values from the text fields
            string name = NameTextBox.Text;
            string number = NumberTextBox.Text;
            string request = RequestTextBox.Text;

            // Show a popup with the entered information
            MessageBox.Show(string.Format("Name: {0}\nNumber: {1}\nRequest: {2}", name, number, request), "Submitted Information");
        }
    }
}

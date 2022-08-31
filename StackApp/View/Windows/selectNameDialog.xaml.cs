using StackApp.ViewModel;
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
using System.Windows.Shapes;

namespace StackApp.View.Windows
{
    /// <summary>
    /// Interaction logic for selectNameDialog.xaml
    /// </summary>
    public partial class selectNameDialog : Window
    {
        public BaseViewModel ViewModel { get; set; }
        public selectNameDialog(BaseViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            inputTextBox.Focus();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = inputTextBox.Text.Trim().ToString();

            if (name == string.Empty || name == null)
            {
                //MessageBox
                DialogResult = false;
                Close();
            }
            else
            {
                ViewModel.SelectedName = name;
                DialogResult = true;
                Close();
            }
        }
    }
}

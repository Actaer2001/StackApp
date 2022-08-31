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
    /// Interaction logic for SelectedNameLoadDialog.xaml
    /// </summary>
    public partial class SelectedNameLoadDialog : Window
    {
        public BaseViewModel ViewModel { get; set; }
        public SelectedNameLoadDialog(BaseViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            inputTextBox.Focus();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
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

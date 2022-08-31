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
    /// Interaction logic for DocsQWindow.xaml
    /// </summary>
    public partial class DocsQWindow : Window
    {
        BaseViewModel viewModel;
        public DocsQWindow(BaseViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedLanguage = "English";
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedLanguage = "Srpski";
            Close();
        }
    }
}

using StackApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ArrayWindow.xaml
    /// </summary>
    public partial class ArrayWindow : Window
    {
        ArrayVM viewModel;
        public ArrayWindow()
        {
            InitializeComponent();

            viewModel = Resources["vm"] as ArrayVM;
            viewModel.CurrentWindow = this;
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            DocsQWindow docsQWindow = new DocsQWindow(viewModel);
            docsQWindow.ShowDialog();
            var filePath = (viewModel.SelectedLanguage == "English") ? System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\Stack_Documentation.pdf") : System.IO.Path.Combine(Environment.CurrentDirectory, @"Resources\Stek_dokumentacija.pdf");
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = filePath,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }

        // Changing the border radius of buttons
        private void ButtonLoaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button b))
                return;

            Border border = default;
            for (var i = 0; null == border && i < VisualTreeHelper.GetChildrenCount(b); i++)
                border = VisualTreeHelper.GetChild(b, i) as Border;

            if (border != null)
                border.CornerRadius = new CornerRadius(3);
        }
    }
}

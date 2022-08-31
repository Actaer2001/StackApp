using StackApp.Model;
using StackApp.ViewModel.ArrayMethods;
using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace StackApp.ViewModel.Commands
{
    public class ArrayPushCommand : ICommand
    {
        public ArrayVM ViewModel { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ArrayPushCommand(ArrayVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.CurrentTextBoxValue == String.Empty || ViewModel.CurrentTextBoxValue == null)
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            ArrayStackMethods.Push(ViewModel);
            ViewModel.CurrentTextBoxValue = String.Empty;
            ViewModel.SelectedNode = null;
        }
    }
}

using StackApp.ViewModel.ArrayMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class ArrayClearCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ArrayVM ViewModel { get; set; }

        public ArrayClearCommand(ArrayVM viewModel)
        {
            ViewModel = viewModel;  
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.StackNodes.Count > 0)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            ArrayStackMethods.ClearStack(ViewModel);
        }
    }
}

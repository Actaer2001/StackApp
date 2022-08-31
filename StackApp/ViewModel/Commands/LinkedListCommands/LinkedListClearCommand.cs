using StackApp.ViewModel.LinkedListMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class LinkedListClearCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LinkedListVM ViewModel { get; set; }

        public LinkedListClearCommand(LinkedListVM viewModel)
        {
            ViewModel = viewModel;  
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.LinkedListNodes.Count > 0 && ViewModel.OperationFlag)
                return true;
            return false;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.OperationFlag)
                LinkedListStackMethods.ClearStack(ViewModel);
        }
    }
}

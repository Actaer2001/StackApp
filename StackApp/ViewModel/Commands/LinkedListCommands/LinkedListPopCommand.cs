using StackApp.Model;
using StackApp.ViewModel.LinkedListMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class LinkedListPopCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LinkedListVM ViewModel { get; set; }

        public LinkedListPopCommand(LinkedListVM viewModel)
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
                LinkedListStackMethods.Pop(ViewModel);
        }
    }
}

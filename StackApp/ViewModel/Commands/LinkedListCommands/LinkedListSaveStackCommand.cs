using StackApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class LinkedListSaveStackCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LinkedListVM ViewModel { get; set; }

        public LinkedListSaveStackCommand(LinkedListVM viewModel)
        {
            this.ViewModel = viewModel;  
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
            {
                selectNameDialog selectNameDialog = new selectNameDialog(ViewModel);
                var result = selectNameDialog.ShowDialog();
                if (result.HasValue)
                    if (result.Value)
                        ViewModel.SaveStack();
            }
        }
    }
}

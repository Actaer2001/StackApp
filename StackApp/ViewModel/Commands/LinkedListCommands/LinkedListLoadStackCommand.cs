using StackApp.View.Windows;
using StackApp.ViewModel.LinkedListMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class LinkedListLoadStackCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LinkedListVM ViewModel { get; set; }

        public LinkedListLoadStackCommand(LinkedListVM vm)
        {
            ViewModel = vm;  
        }

        public bool CanExecute(object parameter)
        {
            return ViewModel.OperationFlag;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.OperationFlag)
            {
                SelectedNameLoadDialog selectedNameLoadDialog = new SelectedNameLoadDialog(ViewModel);
                var result = selectedNameLoadDialog.ShowDialog();
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        ViewModel.LoadStack();
                    }
                }
            }
        }
    }
}

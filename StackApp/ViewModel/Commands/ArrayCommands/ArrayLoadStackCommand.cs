using StackApp.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class ArrayLoadStackCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ArrayVM ViewModel { get; set; }

        public ArrayLoadStackCommand(ArrayVM viewModel)
        {
            ViewModel = viewModel;  
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
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

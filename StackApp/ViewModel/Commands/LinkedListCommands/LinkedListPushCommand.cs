using StackApp.Model;
using StackApp.ViewModel.Helpers;
using StackApp.ViewModel.LinkedListMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StackApp.ViewModel.Commands
{
    public class LinkedListPushCommand : ICommand
    {
        public LinkedListVM ViewModel { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //public event EventHandler CanExecuteChanged;

        public LinkedListPushCommand(LinkedListVM vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.CurrentTextBoxValue == String.Empty || ViewModel.CurrentTextBoxValue == null)
                return false;
            return ViewModel.OperationFlag;
        }

        public async void Execute(object parameter)
        {
            if (ViewModel.OperationFlag)
            {
                ViewModel.OperationFlag = false;
                string value = ViewModel.CurrentTextBoxValue.ToString();
                ViewModel.CurrentTextBoxValue = String.Empty;
                ViewModel.SelectedNode = null;
                ViewModel.CurrentWindow.mainTextBox.IsReadOnly = true;
                if (LinkedListStackMethods.Push(ViewModel, value))
                {
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    ViewModel.OperationFlag = true;
                    ViewModel.CurrentWindow.mainTextBox.IsReadOnly = false;
                }
                else
                {
                    MessageBox.Show("Greška prilikom ubacivanja elementa u stek.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

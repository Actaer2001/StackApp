using StackApp.Model;
using StackApp.Model.Shapes;
using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace StackApp.ViewModel.LinkedListMethods
{
    public class LinkedListStackMethods
    {
        public static bool GetStack(LinkedListVM stackVM)
        {
            var stacks = DatabaseHelper.Read<Stack>();
            if (stacks == null)
            {
                MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (stacks.Any(s => s.Name.ToLower() == stackVM.SelectedName.ToLower()))
            {
                var stack = stacks.Where(s => s.Name.ToLower() == stackVM.SelectedName.ToLower()).ToList().FirstOrDefault();
                if (stack != null)
                {
                    stackVM.SelectedStack = stack;
                    stackVM.TopPointer = stack.TopValue;
                    stackVM.SelectedNode = null;
                    return true;
                }
                MessageBox.Show("Greška prilikom učitavanja steka iz baze.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                MessageBox.Show("Stek koji ste tražili se ne nalazi u našoj bazi podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool Push(LinkedListVM stackVM, string value)
        {
            bool response = false;
            int startValue = stackVM.LinkedListNodes.Count;
            StackPanel node = NodeBlock.NodeBlockMaker(value, 300, 80);

            stackVM.LinkedListNodes.Add(node);
            if (startValue < stackVM.LinkedListNodes.Count)
            {
                stackVM.TopPointer += 1;
                response = true;
            }

            return response;
        }

        public static async void Pop(LinkedListVM stackVM)
        {
            stackVM.OperationFlag = false;
            StackPanel node = stackVM.LinkedListNodes[stackVM.TopPointer];

            var list = stackVM.CurrentWindow.mainCanvas.Children.OfType<StackPanel>().ToList();
            if (list.Count > 1)
            {
                stackVM.CurrentWindow.mainCanvas.Children.Remove(stackVM.CurrentWindow.mainCanvas.Children.OfType<Line>().ToList().Last());
                var list2 = stackVM.CurrentWindow.mainCanvas.Children.OfType<StackPanel>().ToList();
                if (list2.Count > 0)
                    stackVM.CurrentWindow.mainCanvas.Children.Remove(list2.Last());
            }
                stackVM.LinkedListNodes.RemoveAt(stackVM.TopPointer);
                stackVM.CurrentWindow?.mainCanvas.Children.Remove(node);
            list = stackVM.CurrentWindow.mainCanvas.Children.OfType<StackPanel>().ToList();
            if (list.Count > 0)
            {
                LinkPointHelper.CreateLinkLine(stackVM.CurrentWindow.Top, 0, stackVM.CurrentWindow.mainCanvas.Children.OfType<StackPanel>().ToList().Last(), 2, stackVM.CurrentWindow.mainCanvas);
                foreach (StackPanel elem in list)
                    CanvasHelper.move(stackVM, elem, Canvas.GetLeft(elem), Canvas.GetTop(elem), Canvas.GetLeft(elem) - 130, Canvas.GetTop(elem), 1);
            }
            else
            {
                stackVM.CurrentWindow.mainCanvas.Children.Remove(stackVM.CurrentWindow.mainCanvas.Children.OfType<Line>().ToList().FirstOrDefault());
            }
            await Task.Delay(TimeSpan.FromSeconds(1));
            stackVM.SelectedNode = node;
            stackVM.TopPointer -= 1;
            stackVM.OperationFlag = true;
        }

        public static void ClearStack(LinkedListVM stackVM)
        {
            stackVM.SelectedNode = null;
            stackVM.TopPointer = -1;
            stackVM.LinkedListNodes.Clear();
            foreach (var node in stackVM.CurrentWindow.mainCanvas.Children.OfType<StackPanel>().ToList())
                stackVM.CurrentWindow.mainCanvas.Children.Remove(node);
            foreach (var line in stackVM.CurrentWindow.mainCanvas.Children.OfType<Line>().ToList())
                stackVM.CurrentWindow.mainCanvas.Children.Remove(line);
        }
    }
}

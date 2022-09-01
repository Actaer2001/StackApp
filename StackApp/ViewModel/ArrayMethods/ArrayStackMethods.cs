using StackApp.Model;
using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StackApp.ViewModel.ArrayMethods
{
    public class ArrayStackMethods
    {
        public static bool GetStack(ArrayVM stackVM)
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
        public static async void Push(ArrayVM stackVM)
        {
            Node node = ArrayNodeMethods.CreateNode(stackVM.CurrentTextBoxValue, (stackVM.TopPointer + 1));

            var ellipse = new Ellipse()
            {
                Height = 47,
                Width = 47,
                Stroke = Brushes.Blue,
                StrokeThickness = 3
            };
            Canvas.SetLeft(ellipse, 50);
            Canvas.SetTop(ellipse, 56);
            stackVM.CurrentWindow.mainCanvas.Children.Add(ellipse);

            await Task.Delay(TimeSpan.FromSeconds(1));
            stackVM.TopPointer += 1;
            await Task.Delay(TimeSpan.FromSeconds(1));
            stackVM.StackNodes.Add(node);
            stackVM.CurrentWindow.mainCanvas.Children.Remove(ellipse);
        }

        public static async void Pop(ArrayVM stackVM)
        {
            Node node = stackVM.StackNodes[stackVM.TopPointer];
            int startValue = stackVM.StackNodes.Count;

            var ellipse = new Ellipse()
            {
                Height = 47,
                Width = 47,
                Stroke = Brushes.Red,
                StrokeThickness = 3
            };
            Canvas.SetLeft(ellipse, 50);
            Canvas.SetTop(ellipse, 56);
            stackVM.CurrentWindow.mainCanvas.Children.Add(ellipse);
            await Task.Delay(TimeSpan.FromSeconds(1));
            stackVM.TopPointer -= 1;
            await Task.Delay(TimeSpan.FromSeconds(1));
            stackVM.StackNodes.RemoveAt(stackVM.TopPointer + 1);
            stackVM.CurrentWindow.mainCanvas.Children.Remove(ellipse);
            var ellipse2 = new Ellipse()
            {
                Height = 49,
                Width = 71,
                Stroke = Brushes.Blue,
                StrokeThickness = 3
            };
            Canvas.SetLeft(ellipse2, 203);
            Canvas.SetTop(ellipse2, 54);
            stackVM.CurrentWindow.mainCanvas.Children.Add(ellipse2);
            stackVM.SelectedNode = node;
            await Task.Delay(TimeSpan.FromSeconds(1));
            stackVM.CurrentWindow.mainCanvas.Children.Remove(ellipse2);
        }

        public static void ClearStack(ArrayVM stackVM)
        {
            stackVM.SelectedNode = null;
            stackVM.TopPointer = -1;
            stackVM.StackNodes.Clear();
        }

        public static void scroll_move(ListView listView)
        {
            Decorator border = VisualTreeHelper.GetChild(listView, 0) as Decorator;

            if (border != null)
            {
                ScrollViewer scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToRightEnd();
                }
            }
        }
    }
}

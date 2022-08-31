using StackApp.Model;
using StackApp.View.Windows;
using StackApp.ViewModel.Commands;
using StackApp.ViewModel.Helpers;
using StackApp.ViewModel.LinkedListMethods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace StackApp.ViewModel
{
    public class LinkedListVM : BaseViewModel, INotifyPropertyChanged
    {
        private int topPointer;

        public int TopPointer
        {
            get { return topPointer; }
            set
            {
                topPointer = value;
                OnPropertyChanged("TopPointer");
            }
        }
        public ObservableCollection<StackPanel> LinkedListNodes { get; set; }

        private string currentTextBoxValue;

        public string CurrentTextBoxValue
        {
            get { return currentTextBoxValue; }
            set
            {
                currentTextBoxValue = value;
                OnPropertyChanged("CurrentTextBoxValue");
            }
        }


        private StackPanel selectedNode;

        public StackPanel SelectedNode
        {
            get { return selectedNode; }
            set
            {
                selectedNode = value;
                OnPropertyChanged("SelectedNode");
            }
        }


        private Stack selectedStack;

        public Stack SelectedStack
        {
            get { return selectedStack; }
            set
            {
                selectedStack = value;
                OnPropertyChanged("SelectedStack");
                LinkedListNodeMethods.GetNodes(this);
            }
        }

        private bool operationFlag;

        public bool OperationFlag
        {
            get { return operationFlag; }
            set { operationFlag = value; }
        }

        private LinkedListWindow currentWindow;

        public LinkedListWindow CurrentWindow
        {
            get { return currentWindow; }
            set { currentWindow = value; }
        }


        public LinkedListPushCommand PushCommand { get; set; }
        public LinkedListPopCommand PopCommand { get; set; }
        public LinkedListSaveStackCommand SaveStackCommand { get; set; }
        public LinkedListLoadStackCommand LoadStackCommand { get; set; }
        public LinkedListClearCommand ClearCommand { get; set; }

        public LinkedListVM()
        {
            PushCommand = new LinkedListPushCommand(this);
            PopCommand = new LinkedListPopCommand(this);
            SaveStackCommand = new LinkedListSaveStackCommand(this);
            LoadStackCommand = new LinkedListLoadStackCommand(this);
            ClearCommand = new LinkedListClearCommand(this);


            LinkedListNodes = new ObservableCollection<StackPanel>();
            SelectedName = string.Empty;
            TopPointer = -1;
            OperationFlag = true;
            LinkedListNodes.CollectionChanged += LinkedListNodes_CollectionChanged;
        }

        private async void LinkedListNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var item = e?.NewItems?[0] as StackPanel;

            if (item != null)
            {
                var list = CurrentWindow.mainCanvas.Children.OfType<StackPanel>().ToList();
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    CurrentWindow.mainCanvas.Children.Add(item);

                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    foreach (StackPanel node in list)
                        CanvasHelper.move(this, node, Canvas.GetLeft(node), Canvas.GetTop(node), Canvas.GetLeft(node) + 130, Canvas.GetTop(node), 0.5);

                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    if (list.Count > 0)
                    {
                        CurrentWindow.mainCanvas.Children.Remove(CurrentWindow.mainCanvas.Children.OfType<Line>().Last());
                        LinkPointHelper.CreateLinkLine(item, 0, list[list.Count - 1], 1, CurrentWindow.mainCanvas);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    LinkPointHelper.CreateLinkLine(item, 2, CurrentWindow.Top, 0, CurrentWindow.mainCanvas);
                    await Task.Delay(TimeSpan.FromSeconds(0.5));
                    CanvasHelper.move(this, item, Canvas.GetLeft(item), Canvas.GetTop(item), 100, 250, 1);
                }
            }
        }

        public void SaveStack()
        {

            Stack newStack = new Stack
            {
                Name = SelectedName,
                NumberOfNodes = LinkedListNodes.Count,
                TopValue = TopPointer
            };

            var stacks = DatabaseHelper.Read<Stack>();
            if (stacks == null)
            {
                MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (stacks.Any(s => s.Name == SelectedName))
            {
                var stack = DatabaseHelper.Read<Stack>().Where(s => s.Name.ToLower() == SelectedName.ToLower()).ToList().FirstOrDefault();
                newStack.Id = stack.Id;

                for (var i = stack.NumberOfNodes; i < LinkedListNodes.Count; i++)
                {
                    var node = new Node()
                    {
                        StackId = stack.Id,
                        Value = ((LinkedListNodes[i].Children[0] as Border).Child as TextBlock).Text,
                        Index = i
                    };
                    if (!DatabaseHelper.Insert(node))
                    {
                        MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (!DatabaseHelper.Update(newStack))
                {
                    MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                if (!DatabaseHelper.Insert(newStack))
                {
                    MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                for (int i = 0; i < LinkedListNodes.Count; i++)
                {
                    var node = new Node()
                    {
                        StackId = newStack.Id,
                        Value = ((LinkedListNodes[i].Children[0] as Border).Child as TextBlock).Text,
                        Index = i
                    };
                    if (!DatabaseHelper.Insert(node))
                    {
                        MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            LinkedListNodeMethods.GetNodes(this);
            SelectedName = string.Empty;
        }

        public void LoadStack()
        {
            if (LinkedListStackMethods.GetStack(this))
            {
                MessageBox.Show("Stek uspešno učitan.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

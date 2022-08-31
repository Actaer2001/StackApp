using StackApp.Model;
using StackApp.View.Windows;
using StackApp.ViewModel.ArrayMethods;
using StackApp.ViewModel.Commands;
using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StackApp.ViewModel
{
    public class ArrayVM : BaseViewModel, INotifyPropertyChanged
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

        private ObservableCollection<Node> stackNodes;

        public ObservableCollection<Node> StackNodes
        {
            get { return stackNodes; }
            set
            {
                stackNodes = value;
            }
        }


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


        private Node selectedNode;

        public Node SelectedNode
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
                ArrayNodeMethods.GetNodes(this);
            }
        }

        private ArrayWindow currentWindow;

        public ArrayWindow CurrentWindow
        {
            get { return currentWindow; }
            set { currentWindow = value; }
        }

        public ArrayPushCommand PushCommand { get; set; }
        public ArrayPopCommand PopCommand { get; set; }
        public ArraySaveStackCommand SaveStackCommand { get; set; }
        public ArrayLoadStackCommand LoadStackCommand { get; set; }
        public ArrayClearCommand ClearCommand { get; set; }

        public ArrayVM()
        {

            PushCommand = new ArrayPushCommand(this);
            PopCommand = new ArrayPopCommand(this);
            SaveStackCommand = new ArraySaveStackCommand(this);
            LoadStackCommand = new ArrayLoadStackCommand(this);
            ClearCommand = new ArrayClearCommand(this);


            StackNodes = new ObservableCollection<Node>();
            SelectedName = string.Empty;
            TopPointer = -1;
            StackNodes.CollectionChanged += StackNodes_CollectionChanged;
        }

        private void StackNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ArrayStackMethods.scroll_move(CurrentWindow.mainListView);
        }

        public void SaveStack()
        {

            Stack newStack = new Stack
            {
                Name = SelectedName,
                NumberOfNodes = StackNodes.Count,
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

                for (var i = stack.NumberOfNodes; i < StackNodes.Count; i++)
                {
                    StackNodes[i].StackId = stack.Id;
                    if (!DatabaseHelper.Insert(StackNodes[i]))
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

                foreach (Node node in StackNodes)
                {
                    node.StackId = newStack.Id;
                    if (!DatabaseHelper.Insert(node))
                    {
                        MessageBox.Show("Greška u konekciji sa bazom podataka.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            ArrayNodeMethods.GetNodes(this);
            SelectedName = string.Empty;
        }

        public void LoadStack()
        {
            if (ArrayStackMethods.GetStack(this))
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

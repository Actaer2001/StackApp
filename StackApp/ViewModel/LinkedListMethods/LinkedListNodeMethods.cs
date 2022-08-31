using StackApp.Model;
using StackApp.Model.Shapes;
using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackApp.ViewModel.LinkedListMethods
{
    public class LinkedListNodeMethods
    {
        public static Node CreateNode(string value, int index)
        {
            Node newNode = new Node
            {
                Value = value,
                Index = index
            };

            return newNode;
        }

        public static async void GetNodes(LinkedListVM stackVM)
        {
            if (stackVM.SelectedStack != null)
            {
                stackVM.OperationFlag = false;
                stackVM.CurrentWindow.mainTextBox.IsReadOnly = true;
                var nodes = DatabaseHelper.Read<Node>().Where(n => n.StackId == stackVM.SelectedStack.Id).OrderBy(n => n.Index).ToList();

                LinkedListStackMethods.ClearStack(stackVM);

                foreach (var node in nodes)
                {
                    stackVM.LinkedListNodes.Add(NodeBlock.NodeBlockMaker(node.Value, 300, 80));
                    await Task.Delay(TimeSpan.FromSeconds(3.5));
                }
                stackVM.CurrentWindow.mainTextBox.IsReadOnly = false;
                stackVM.OperationFlag = true;
            }
        }
    }
}

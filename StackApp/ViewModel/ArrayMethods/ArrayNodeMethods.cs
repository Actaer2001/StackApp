using StackApp.Model;
using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackApp.ViewModel.ArrayMethods
{
    public class ArrayNodeMethods
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

        public static void GetNodes(ArrayVM stackVM)
        {
            if (stackVM.SelectedStack != null)
            {
                var nodes = DatabaseHelper.Read<Node>().Where(n => n.StackId == stackVM.SelectedStack.Id).OrderBy(n => n.Index).ToList();

                stackVM.StackNodes.Clear();
                //for(int i = 0; i < nodes.Count; i++)
                //{
                //    var node = (Node)nodes.Where(n => n.Index == i);
                //    stackVM.StackNodes.Add(node);
                //}

                foreach (var node in nodes)
                    stackVM.StackNodes.Add(node);
            }
        }
    }
}

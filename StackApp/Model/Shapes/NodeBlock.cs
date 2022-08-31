using StackApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StackApp.Model.Shapes
{
    public class NodeBlock
    {
        public static StackPanel NodeBlockMaker(string value, double left, double top)
        {
            if (value == null)
                value = "Value";
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            var textBlock = new TextBlock()
            {
                Text = value,
                Padding = new Thickness(15, 10, 15, 10),
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.DemiBold,
                MaxWidth = 70
            };
            var textBlockBorder = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1)
            };
            var addOnBorder = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                Height = 40,
                Width = 25
            };
            textBlockBorder.Child = textBlock;
            stackPanel.Children.Add(textBlockBorder);
            stackPanel.Children.Add(addOnBorder);
            Canvas.SetLeft(stackPanel, left);
            Canvas.SetTop(stackPanel, top);
            LinkPointHelper.SetLinkPoints(stackPanel, new List<Point>());
            LinkPointHelper.AddLinkPoint(stackPanel, new Point(55, 20));
            LinkPointHelper.AddLinkPoint(stackPanel, new Point(0, 20));
            LinkPointHelper.AddLinkPoint(stackPanel, new Point(25, 0));
            return stackPanel;
        }
    }
}

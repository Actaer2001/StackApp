using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace StackApp.ViewModel.Helpers
{
    public class CanvasHelper
    {
        public static async void move(LinkedListVM VM, FrameworkElement target, double oldX, double oldY, double newX, double newY, double time)
        {
            VM.OperationFlag = false;
            if (double.IsNaN(oldX))
                oldX = 0.0;
            if (double.IsNaN(oldY))
                oldY = 0.0;
            if (double.IsNaN(newX))
                newX = 0.0;
            if (double.IsNaN(newY))
                newY = 0.0;
            var offsetX = new DoubleAnimation()
            {
                From = oldX,
                To = newX,
                Duration = TimeSpan.FromSeconds(time)
            };

            var offsetY = new DoubleAnimation()
            {
                From = oldY,
                To = newY,
                Duration = TimeSpan.FromSeconds(time)
            };

            Storyboard sb = new Storyboard();
            sb.Children.Add(offsetX);
            sb.Children.Add(offsetY);
            Storyboard.SetTarget(sb.Children.ElementAt(0) as DoubleAnimation, target);
            Storyboard.SetTarget(sb.Children.ElementAt(1) as DoubleAnimation, target);
            Storyboard.SetTargetProperty(sb.Children.ElementAt(0) as DoubleAnimation, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(sb.Children.ElementAt(1) as DoubleAnimation, new PropertyPath("(Canvas.Top)"));
            sb.Begin();
            await Task.Delay(TimeSpan.FromSeconds(time));
            VM.OperationFlag = true;
        }
    }
}

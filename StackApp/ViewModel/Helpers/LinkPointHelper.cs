using StackApp.Model.Shapes;
using StackApp.ViewModel.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StackApp.ViewModel.Helpers
{
    public class LinkPointHelper
    {
        public static readonly DependencyProperty LinkPointsProperty = DependencyProperty.RegisterAttached("LinkPoints", typeof(List<Point>), typeof(FrameworkElement), new PropertyMetadata(new List<Point>()));

        public static List<Point> GetLinkPoints(FrameworkElement element)
        {
            return (List<Point>)element.GetValue(LinkPointsProperty);
        }

        public static void SetLinkPoints(FrameworkElement element, List<Point> value)
        {
            element.SetValue(LinkPointsProperty, value);
        }

        public static void AddLinkPoint(FrameworkElement element, Point p)
        {
            GetLinkPoints(element).Add(p);
        }

        public static void CreateLinkLine(FrameworkElement element1, int index1, FrameworkElement element2, int index2, Canvas container)
        {
            Line line = new Line();
            line.StrokeEndLineCap = PenLineCap.Triangle;

            // Bind line.X1 to element1.LinkPoint[index1].X + element1.Canvas.Left:
            MultiBinding mbx1 = new MultiBinding();
            Binding bx11 = new Binding();
            bx11.Source = element1;
            bx11.Path = new PropertyPath("(FrameworkElement.LinkPoints)[" + index1 + "].X");
            mbx1.Bindings.Add(bx11);

            Binding bx12 = new Binding();
            bx12.Source = element1;
            bx12.Path = new PropertyPath("(Canvas.Left)");
            mbx1.Bindings.Add(bx12);

            OffsetConverter converterx1 = new OffsetConverter();
            mbx1.Converter = converterx1;
            line.SetBinding(Line.X1Property, mbx1);


            MultiBinding mby1 = new MultiBinding();
            Binding by11 = new Binding();
            by11.Source = element1;
            by11.Path = new PropertyPath("(FrameworkElement.LinkPoints)[" + index1 + "].Y");
            mby1.Bindings.Add(by11);

            Binding by12 = new Binding();
            by12.Source = element1;
            by12.Path = new PropertyPath("(Canvas.Top)");
            mby1.Bindings.Add(by12);

            OffsetConverter convertery1 = new OffsetConverter();
            mby1.Converter = convertery1;
            line.SetBinding(Line.Y1Property, mby1);


            MultiBinding mbx2 = new MultiBinding();
            Binding bx21 = new Binding();
            bx21.Source = element2;
            bx21.Path = new PropertyPath("(FrameworkElement.LinkPoints)[" + index2 + "].X");
            mbx2.Bindings.Add(bx21);

            Binding bx22 = new Binding();
            bx22.Source = element2;
            bx22.Path = new PropertyPath("(Canvas.Left)");
            mbx2.Bindings.Add(bx22);

            OffsetConverter converterx2 = new OffsetConverter();
            mbx2.Converter = converterx2;
            line.SetBinding(Line.X2Property, mbx2);


            MultiBinding mby2 = new MultiBinding();
            Binding by21 = new Binding();
            by21.Source = element2;
            by21.Path = new PropertyPath("(FrameworkElement.LinkPoints)[" + index2 + "].Y");
            mby2.Bindings.Add(by21);

            Binding by22 = new Binding();
            by22.Source = element2;
            by22.Path = new PropertyPath("(Canvas.Top)");
            mby2.Bindings.Add(by22);

            OffsetConverter convertery2 = new OffsetConverter();
            mby2.Converter = convertery2;
            line.SetBinding(Line.Y2Property, mby2);


            line.Stroke = Brushes.Black;
            container.Children.Add(line);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;


namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int last = 0;

        public MainWindow()
        {
            InitializeComponent();
            Random rnd = new Random();
            ArrayList a = new ArrayList{ new Point(1000, 20) };
            for (int i = 0; i < 1000; i++)
            {
                int r = rnd.Next(1, 1000);
                int l = rnd.Next(1, 50);
                DateTime dt = DateTime.Now;
                int x = (dt.Millisecond + rnd.Next(0, 100)) % 20;
                a.Add(new Point(r + last, x));
                last = r + last;
            }
            System.Collections.Generic.IEnumerable<Point> g = (IEnumerable<Point>)a.ToArray(typeof(Point));
            var dataSource = new EnumerableDataSource<Point>(g);
            dataSource.SetXMapping(x => x.X);
            dataSource.SetYMapping(y => y.Y);
            plotter.AddLineGraph(dataSource, new Pen(Brushes.Green, 1), new PenDescription("Data"));
            //plotter.Children.Remove(plotter.MouseNavigation);
            // plotter.FitToView();
        }

      

        private void plotter_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                Point mousePos = e.GetPosition(this);
                Point zoomTo = mousePos.ScreenToViewport(plotter.Viewport.Transform);

                double zoomSpeed = Math.Abs(e.Delta / Mouse.MouseWheelDeltaForOneLine);
                zoomSpeed *= 0.2;
                if (e.Delta < 0)
                {
                    zoomSpeed = 1 / zoomSpeed;
                }

              //  plotter.Viewport.SetChangeType(ChangeType.Zoom);
                plotter.Viewport.Visible = plotter.Viewport.Visible.ZoomX(zoomTo, zoomSpeed);
                //plotter.Viewport.SetChangeType();
                e.Handled = true;
            }
        }
    }
}

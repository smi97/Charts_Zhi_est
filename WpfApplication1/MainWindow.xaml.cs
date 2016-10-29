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
using KBCsv;
using System.IO;
using System.Globalization;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        static int y = 0, x = 62500000;

        public EnumerableDataSource<Point> getSource()
        {
            Random rnd = new Random();
            ArrayList a = new ArrayList();
            for (int i = 0; i < 20; i++)
            {
                x += rnd.Next(1, 1000);
                DateTime dt = DateTime.Now;
                y = (dt.Millisecond + rnd.Next(0, 100)) % 20;
                a.Add(new Point(x, y));
            }
            System.Collections.Generic.IEnumerable<Point> g = (IEnumerable<Point>)a.ToArray(typeof(Point));
            var dataSource = new EnumerableDataSource<Point>(g);
            dataSource.SetXMapping(x => x.X);
            dataSource.SetYMapping(y => y.Y);
            return dataSource;
        }



        LineGraph chart = new LineGraph();
        Pen curColour = new Pen(Brushes.Green, 1.0);
        Pen newColour = new Pen(Brushes.Red, 1.0);

        public double mSec(int y, int mon, int day, int h, int m, int s, int ms)
        {
            DateTime date = new DateTime(y,mon,day, h, m,s,ms);
            return (double)(date.Ticks)/ 10000000000;
        }

        static ArrayList getData()
        {
            using (var textReader = new StreamReader("Ticks.csv"))
            using (var reader = new CsvReader(textReader, true))
            {
                ArrayList list = new ArrayList();
                reader.ValueSeparator = ';';   // this will be used between each value
                while (reader.HasMoreRecords)
                {
                    var dataRecord = reader.ReadDataRecord();
                    // since the reader has a header record, we can access data by column names as well as by index
                    dataRecord[1] = dataRecord[1].Replace(".", ",");
                    DateTime dt = DateTime.ParseExact(dataRecord[0], "yyyy.MM.dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                    list.Add(new Point((double)(dt.Ticks) / 10000000000, Convert.ToDouble(dataRecord[1])));
                }
                return list;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            ArrayList a = getData();

            var rt = new EnumerableDataSource<Point>((IEnumerable<Point>)a.ToArray(typeof(Point)));
            rt.SetXMapping(x => x.X);
            rt.SetYMapping(y => y.Y);
            chart = plotter.AddLineGraph(rt, curColour, new PenDescription("Data"));
            plotter.FitToView();
        }

        private void screenShot(object sender, RoutedEventArgs e)
        {
            plotter.CreateScreenshot();
            plotter.SaveScreenshot("a.jpg");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            curColour = chart.LinePen;
            chart.LinePen = newColour;
            newColour = curColour;
        }



        private void plotter_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                Point mousePos = e.GetPosition(this);
                Point zoomTo = mousePos.ScreenToViewport(plotter.Viewport.Transform);

                double zoomSpeed = Math.Abs(e.Delta / Mouse.MouseWheelDeltaForOneLine);
                zoomSpeed *= 1.2;
                if (e.Delta < 0)
                {
                    zoomSpeed = 1 / zoomSpeed;
                }
                //plotter.Viewport.SetChangeType(ChangeType.Zoom);
                plotter.Viewport.Visible = plotter.Viewport.Visible.ZoomX(zoomTo, zoomSpeed);
                //plotter.Viewport.SetChangeType();
                e.Handled = true;
            }
        }



    }
}

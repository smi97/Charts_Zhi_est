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
using Microsoft.Research.DynamicDataDisplay.Charts;
using System.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        static string path = "Ticks_test.csv";
        LineGraph chart = new LineGraph();    // main chart
        LineGraph barchart = new LineGraph(); // traded lot volume chart
        static DateTime dtmin = new DateTime();
        static DateTime dtmax = new DateTime();
        static double ticmin = 0.0;
        static double ticmax = 0.0;



        Pen curColour = new Pen(Brushes.Blue, 1.0);
        Pen newColour = new Pen(Brushes.Gray, 1.0);

        public MainWindow()
        {
            InitializeComponent();

            Window1 load = new Window1();
            SplashScreen splash = new SplashScreen("dv.jpg");

            if (load.ShowDialog() == true)
            {
                path = load.path;
                if (!File.Exists(path))
                {
                    var result = MessageBox.Show("No File");
                    Application.Current.Shutdown();
                }
                splash.Show(false);
            }
            else
            {
                Application.Current.Shutdown();
            }

            plotter1.Background = Brushes.Transparent;
            plotter1.BorderBrush = Brushes.Transparent;
            plotter1.AxisGrid.Visibility = Visibility.Hidden;

            ArrayList vol = new ArrayList();  // traded lot volume
            ArrayList a = getData(ref vol);

            /* --- Setting charts  --- */

            var rt = new EnumerableDataSource<Point>((IEnumerable<Point>)a.ToArray(typeof(Point)));
            rt.SetXMapping(x => x.X);
            rt.SetYMapping(y => y.Y);
            chart = plotter.AddLineGraph(rt, curColour, new PenDescription("Data"));


            rt = new EnumerableDataSource<Point>((IEnumerable<Point>)vol.ToArray(typeof(Point)));
            rt.SetXMapping(x => x.X);
            rt.SetYMapping(y => y.Y + ticmin - ticmin / 1000);

            barchart = new LineGraph(rt);
            barchart = plotter1.AddLineGraph(rt, newColour, new PenDescription("Bar"));

            /* ------------------------- */

            /* --- Setting Rect  --- */

            var axis = (DateTimeAxis)plotter.HorizontalAxis;
            double xMin = axis.ConvertToDouble(dtmin);
            double xMax = axis.ConvertToDouble(dtmax);
            Rect visibleRect = new Rect(xMin, ticmin - ticmin / 1000 /* this is yMin */, xMax / 2 - xMin / 2 /* this is width */, ticmax - ticmin + ticmin / 800 /* this is YMax - YMin = height */);
            if (path == "Ticks.csv")
                visibleRect = new Rect(xMin, ticmin + ticmin / 1000 /* this is yMin */, (xMax - xMin) / 3  /* this is width */, ticmax - ticmin * 1.2  /* this is YMax - YMin = height */);

            plotter.Viewport.Visible = visibleRect;
            plotter1.Viewport.Visible = visibleRect;
            plotter.Viewport.SetBinding(Viewport2D.VisibleProperty, new Binding("Visible") { Source = plotter1.Viewport, Mode = BindingMode.TwoWay });

            /* --------------------- */

            splash.Close(new TimeSpan(0, 0, 3));

        }

        static ArrayList getData(ref ArrayList vol)
        {
            using (var textReader = new StreamReader(path))
            using (var reader = new CsvReader(textReader, true))
            {
                double pr = new double();
                double tick = new double();
                double lasttick = new double();
                double ob = 0;
                bool first = true;
                ArrayList list = new ArrayList();
                DataRecord dataRecord = new DataRecord();
                reader.ValueSeparator = ';';   // this will be used between each value
                while (reader.HasMoreRecords)
                {
                    dataRecord = reader.ReadDataRecord();
                    // since the reader has a header record, we can access data by column names as well as by index
                    dataRecord[1] = dataRecord[1].Replace(".", ",");
                    DateTime dt = DateTime.ParseExact(dataRecord[0], "yyyy.MM.dd HH:mm:ss:fff", CultureInfo.InvariantCulture);

                    tick = (double)(dt.Ticks) / 10000000000;
                    pr = Convert.ToDouble(dataRecord[1]);
                    if (first)
                    {
                        dtmin = dt;
                        dtmax = dt;
                        ticmin = pr;
                        ticmax = pr;
                        lasttick = tick;
                        first = false;
                    }
                    if (dt < dtmin) dtmin = dt;
                    if (dt > dtmax) dtmax = dt;

                    if (pr < ticmin) ticmin = pr;
                    if (pr > ticmax) ticmax = pr;
                    list.Add(new Point(tick, pr));



                    if (tick == lasttick)
                    { ob = ob + Convert.ToDouble(dataRecord[2]); }
                    else
                    {
                        if (ob == 0)
                            ob = Convert.ToDouble(dataRecord[2]);

                        Point barpoint = new Point(lasttick, 0);
                        vol.Add(barpoint);

                        barpoint = new Point(lasttick, ob / 2);
                        vol.Add(barpoint);

                        barpoint = new Point(tick, ob / 2);
                        vol.Add(barpoint);
                        lasttick = tick;
                        ob = 0;
                    }
                }
                return list;
            }

        }


        private void screenShot(object sender, RoutedEventArgs e)
        {
            plotter.CreateScreenshot();
            plotter.SaveScreenshot("Screen.jpg");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var axis = (DateTimeAxis)plotter.HorizontalAxis;
            double xMin = axis.ConvertToDouble(dtmin);
            double xMax = axis.ConvertToDouble(dtmax);
            Rect visibleRect = new Rect(xMin, ticmin - ticmin / 1000 /* this is yMin */, xMax / 2 - xMin / 2 /* this is width */, ticmax - ticmin + ticmin / 800 /* this is YMax - YMin = height */);

            plotter.Viewport.Visible = visibleRect;
            plotter1.Viewport.Visible = visibleRect;
            plotter.Viewport.SetBinding(Viewport2D.VisibleProperty, new Binding("Visible") { Source = plotter1.Viewport, Mode = BindingMode.TwoWay });
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var axis = (DateTimeAxis)plotter.HorizontalAxis;
            double xMin = axis.ConvertToDouble(dtmin);
            double xMax = axis.ConvertToDouble(dtmax);
            Rect visibleRect = new Rect(xMin + xMax / 2 - xMin / 2, ticmin - ticmin / 1000 /* this is yMin */, xMax / 2 - xMin / 2 /* this is width */, ticmax - ticmin + ticmin / 800 /* this is YMax - YMin = height */);
            plotter.Viewport.Visible = visibleRect;
            plotter1.Viewport.Visible = visibleRect;
            plotter.Viewport.SetBinding(Viewport2D.VisibleProperty, new Binding("Visible") { Source = plotter1.Viewport, Mode = BindingMode.TwoWay });
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            barchart.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            barchart.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            BarChartBox.SetCurrentValue(CheckBox.IsCheckedProperty, true);
        }
    }
}

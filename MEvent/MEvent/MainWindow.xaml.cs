using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MEvent
{


    //mousemouve 10-25мс
    //mouseClick 60 - 150мс
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public bool record = false;
        List<DateTime> dtimes = new List<DateTime>();
        List<int> mss = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "R")
            {
                record = !record;
                return;
            }
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (record == false)
            {
                return;
            }
            MyEvent my = new MyEvent();
            my.name = "MOVE";
            my.x = e.GetPosition(null).X;
            my.y = e.GetPosition(null).Y;
            my.date = DateTime.Now;
            dtimes.Add(DateTime.Now);


            writeEvent(my);

        }


        private void writeEvent(MyEvent my)
        {
            mss.Clear();
            string text = console.Text;
            console.Text = my.toString() + '\n' + text;



        }

        public class MyEvent
        {
            public string name;
            public double x;
            public double y;
            public DateTime date;

            public string toString()
            {
                return "[" + date.ToString("ss.fff") + "]" + name + " " + x + " : " + y;
            }
            

        }

        private void MainWindow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (record == false)
            {
                return;
            }
            MyEvent my = new MyEvent();
            my.name = "UP";
            my.x = e.GetPosition(null).X;
            my.y = e.GetPosition(null).Y;
            my.date = DateTime.Now;
            dtimes.Add(DateTime.Now);


            writeEvent(my);
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (record == false)
            {
                return;
            }
            MyEvent my = new MyEvent();
            my.name = "DOWN";
            my.x = e.GetPosition(null).X;
            my.y = e.GetPosition(null).Y;
            my.date = DateTime.Now;
            dtimes.Add(DateTime.Now);


            writeEvent(my);
        }

        private void work(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);
            record = true;
            //MouseOperations.mouseClick();
            System.Drawing.Point start = new System.Drawing.Point(400, 400);

            MouseOperations.LinearSmoothMove(start, new TimeSpan(0, 0, 1));
        }


        //            Thread.Sleep(5000);
        //Point start = new Point(100, 100);
        //LinearSmoothMove(start, new TimeSpan(0,0,1));
        //    SendKeys.SendWait("^+{TAB}");
        //    LinearSmoothMove(start, new TimeSpan(0, 0, 1));
    }
}

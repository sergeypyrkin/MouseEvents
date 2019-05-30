using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using Clipboard = System.Windows.Forms.Clipboard;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;

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

            writeMessage("Привет куколд", 0);
            //record = true;
            ////MouseOperations.mouseClick();
            //System.Drawing.Point start = new System.Drawing.Point(400, 400);

            //MouseOperations.LinearSmoothMove(start, new TimeSpan(0, 0, 1));
        }

        public void writeMessage(string mess, int member)
        {
            Clipboard.Clear();
            Clipboard.SetText(mess);

            //ждем немножно
            Thread.Sleep(getRandom(1000, 1500));
            if (member != 0) //переключаемся
            {
                SendKeys.SendWait("^+{TAB}");
            }
            //двигаемся на кнопку
            Thread.Sleep(getRandom(100, 200));

            System.Drawing.Point point1 = new System.Drawing.Point(720 + 20 - getRandom(0, 40), 420 + 5 - getRandom(0, 10));   //+-20  , +-10 
            MouseSimulator.LinearSmoothMove(point1, new TimeSpan(0, 0, 0, 0, getRandom(400, 500)));

            Thread.Sleep(getRandom(10, 50));

            MouseSimulator.ClickLeftMouseButton();

            Thread.Sleep(getRandom(800, 1200));

            System.Drawing.Point point3 = new System.Drawing.Point(750 + 20 - getRandom(0, 40), 450 + 5 - getRandom(0, 10));   //+-20  , +-10 
            MouseSimulator.LinearSmoothMove(point3, new TimeSpan(0, 0, 0, 0, getRandom(100, 200)));

            Thread.Sleep(getRandom(100, 200));

            SendKeys.SendWait("^+{V}");
            Thread.Sleep(getRandom(200, 300));
            System.Drawing.Point point2 = new System.Drawing.Point(1120 +  20 - getRandom(0, 40), 580 + 10 - getRandom(0, 20));   //+-20  , +-10 

            MouseSimulator.LinearSmoothMove(point2, new TimeSpan(0, 0, 0, 0, getRandom(400, 500)));
            Thread.Sleep(getRandom(200, 300));
            MouseSimulator.ClickLeftMouseButton();
        }

        public int getRandom(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }




        //            Thread.Sleep(5000);
        //Point start = new Point(100, 100);
        //LinearSmoothMove(start, new TimeSpan(0,0,1));
        //    SendKeys.SendWait("^+{TAB}");
        //    LinearSmoothMove(start, new TimeSpan(0, 0, 1));
    }
}

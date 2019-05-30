using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using Common;

public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition(int X, int Y)
    {
        SetCursorPos(X, Y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    public static void MouseEvent(MouseEventFlags value)
    {
        MousePoint position = GetCursorPosition();

        mouse_event
        ((int)value,
            position.X,
            position.Y,
            0,
            0);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public  static void mouseClick()
    {
        var point = MouseOperations.GetCursorPosition();
        int x = point.X;
        int y = point.Y;
        MouseOperations.mouse_event((int)(MouseOperations.MouseEventFlags.Absolute | MouseOperations.MouseEventFlags.LeftDown), x, y, 0, 0);
        int sleep = getRandom(60, 150);
        Thread.Sleep(sleep);
        MouseOperations.mouse_event((int)(MouseOperations.MouseEventFlags.Absolute | MouseOperations.MouseEventFlags.LeftUp), x, y, 0, 0);
    }


    public static void mouseMove(int x, int y)
    {

        MouseOperations.mouse_event((int)(MouseOperations.MouseEventFlags.Absolute | MouseOperations.MouseEventFlags.Move), x, y, 0, (int) UIntPtr.Zero);
    }


    public static void LinearSmoothMove(System.Drawing.Point newPosition, TimeSpan duration)
    {
        var point = MouseOperations.GetCursorPosition();
        System.Drawing.Point start = new System.Drawing.Point(point.X, point.Y);

        // Find the vector between start and newPosition
        double deltaX = newPosition.X - start.X;
        double deltaY = newPosition.Y - start.Y;

        // start a timer
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        double timeFraction = 0.0;

        do
        {
            timeFraction = (double)stopwatch.Elapsed.Ticks / duration.Ticks;
            if (timeFraction > 1.0)
                timeFraction = 1.0;

            PointF curPoint = new PointF(Convert.ToInt32(start.X + timeFraction * deltaX),
                Convert.ToInt32(start.Y + timeFraction * deltaY));

            //MouseOperations.SetCursorPos(Convert.ToInt32(curPoint.X), Convert.ToInt32(curPoint.Y));
            // MouseSimulator.MouseMove(Convert.ToInt32(curPoint.X), Convert.ToInt32(curPoint.Y));
            int inputXinPixels = Convert.ToInt32(curPoint.X);
            int inputYinPixels = Convert.ToInt32(curPoint.Y);
            var screenBounds = Screen.PrimaryScreen.Bounds;
            var outputX = inputXinPixels * 65535 / screenBounds.Width;
            var outputY = inputYinPixels * 65535 / screenBounds.Height;
            MouseOperations.mouseMove(outputX, outputY);
            Thread.Sleep(20);
        } while (timeFraction < 1.0);
    }



    public static int getRandom(int min, int max)
    {
        Random rand = new Random();
        return rand.Next(min, max);
    }



}
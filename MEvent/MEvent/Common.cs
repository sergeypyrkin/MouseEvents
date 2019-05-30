
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices;

namespace Common
{


    public class MouseSimulator
    {
        public static void ClickLeftMouseButton()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_LEFTDOWN;
            User32.SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));

            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_LEFTUP;
            User32.SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }
        public static void ClickRightMouseButton()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_RIGHTDOWN;
            User32.SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));

            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_RIGHTUP;
            User32.SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }

        public static void MouseMove(int dx, int dy)
        {
            INPUT mouseMove = new INPUT();
            mouseMove.type = SendInputEventType.InputMouse;
            mouseMove.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_ABSOLUTE | MouseEventFlags.MOUSEEVENT_MOVE;
            mouseMove.mkhi.mi.dx = dx;
            mouseMove.mkhi.mi.dy = dy;
            User32.SendInput(1, ref mouseMove, Marshal.SizeOf(new INPUT()));
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public SendInputEventType type;
        public MOUSEANDKEYBOARDINPUT mkhi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBOARDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSEANDKEYBOARDINPUT
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBOARDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    [Flags]
    public enum MouseEventFlags : uint
    {
        MOUSEEVENT_MOVE = 0x0001,
        MOUSEEVENT_LEFTDOWN = 0x0002,
        MOUSEEVENT_LEFTUP = 0x0004,
        MOUSEEVENT_RIGHTDOWN = 0x0008,
        MOUSEEVENT_RIGHTUP = 0x0010,
        MOUSEEVENT_MIDDLEDOWN = 0x0020,
        MOUSEEVENT_MIDDLEUP = 0x0040,
        MOUSEEVENT_XDOWN = 0x0080,
        MOUSEEVENT_XUP = 0x0100,
        MOUSEEVENT_WHEEL = 0x0800,
        MOUSEEVENT_VIRTUALDESK = 0x4000,
        MOUSEEVENT_ABSOLUTE = 0x8000
    }

    [Flags]
    public enum SendInputEventType : uint
    {
        InputMouse,
        InputKeyboard,
        InputHardware
    }

    public class User32
    {
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT p);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr ptr);
    }


}
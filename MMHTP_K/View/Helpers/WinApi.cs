using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MMHTP_K.View.Helpers
{
    public class WinApi
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        /// <summary>
        /// Найти окно
        /// </summary>
        /// <param name="lpClassName">Имя класса окна</param>
        /// <param name="lpWindowName">Имя окна</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr HWnd, GetWindow_Cmd cmd);

  //      [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)] //
  //      public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
  //      [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
  //      public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam,
  //      int lparam);
  //      [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
  //      public static extern int RegisterWindowMessage(string lpString);

  //      public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

  //      [DllImport("user32.Dll")]
  //      [return: MarshalAs(UnmanagedType.Bool)]
  //      public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

  //      [DllImport("user32.dll", CharSet = CharSet.Auto)]
  //      static public extern IntPtr GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
  //      [DllImport("user32.dll", EntryPoint = "FindWindowEx",
  //CharSet = CharSet.Auto)]
  //      static extern IntPtr FindWindowEx(IntPtr hwndParent,
  //        IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

  //      public static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent,
  //int maxCount)
  //      {
  //          List<IntPtr> result = new List<IntPtr>();
  //          int ct = 0;
  //          IntPtr prevChild = IntPtr.Zero;
  //          IntPtr currChild = IntPtr.Zero;
  //          while (true && ct < maxCount)
  //          {
  //              currChild = FindWindowEx(hParent, prevChild, null, null);
  //              if (currChild == IntPtr.Zero) break;
  //              result.Add(currChild);
  //              prevChild = currChild;
  //              ++ct;
  //          }
  //          return result;
  //      }

  //      private static bool EnumWindow(IntPtr handle, IntPtr pointer)
  //      {
  //          GCHandle gch = GCHandle.FromIntPtr(pointer);
  //          List<IntPtr> list = gch.Target as List<IntPtr>;
  //          if (list == null)
  //              throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
  //          list.Add(handle);
  //          return true;
  //      }

  //      public static List<IntPtr> GetChildWindows(IntPtr parent)
  //      {
  //          List<IntPtr> result = new List<IntPtr>();
  //          GCHandle listHandle = GCHandle.Alloc(result);
  //          try
  //          {
  //              Win32Callback childProc = new Win32Callback(EnumWindow);
  //              EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
  //          }
  //          finally
  //          {
  //              if (listHandle.IsAllocated)
  //                  listHandle.Free();
  //          }
  //          return result;
  //      }

  //      public static string GetWinClass(IntPtr hwnd)
  //      {
  //          if (hwnd == IntPtr.Zero)
  //              return null;
  //          StringBuilder classname = new StringBuilder(100);
  //          IntPtr result = GetClassName(hwnd, classname, classname.Capacity);
  //          if (result != IntPtr.Zero)
  //              return classname.ToString();
  //          return null;
  //      }

  //      public static IEnumerable<IntPtr> EnumAllWindows(IntPtr hwnd, string childClassName)
  //      {
  //          List<IntPtr> children = GetChildWindows(hwnd);
  //          if (children == null)
  //              yield break;
  //          foreach (IntPtr child in children)
  //          {
  //              if (GetWinClass(child) == childClassName)
  //                  yield return child;
  //              foreach (var childchild in EnumAllWindows(child, childClassName))
  //                  yield return childchild;
  //          }
  //      }
        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6,
            WM_GETTEXT = 0x000D,
            WM_GETTEXTLENGTH = 0x000E
        }

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }
        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }
        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        static int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        static int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }

        public async static Task MoveCursorTo(int x, int y)
        {
            System.Drawing.Point cursorPos = System.Windows.Forms.Cursor.Position;

            var dx = cursorPos.X - x;
            var dy = cursorPos.Y - y;
            var deltaX = 1;
            var deltaY = 1;
            if (dx%2 == 0) deltaX *= 2;
            if (dy%2 == 0) deltaY *= 2;
            if (dx > 0) deltaX *= -1;
            if (dy > 0) deltaY *= -1;
            int i = Convert.ToInt32(cursorPos.X), j = Convert.ToInt32(cursorPos.Y);
            while (i!=x||j!=y)
            {
                await Task.Run(() =>
                {
                    SetCursorPos(i, j);
                    if (i != x) i += deltaX;
                    if (j != y) j += deltaY;
                    Thread.Sleep(5);
                });
            }
        }

        public static void ClickLeftMouseButton(int x, int y)
        {
            var mouseInput = new INPUT {type = SendInputEventType.InputMouse};
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;


            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }

        public static void ClickRightMouseButton(int x, int y)
        {
            var mouseInput = new INPUT {type = SendInputEventType.InputMouse};
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;


            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        } 
    }
}

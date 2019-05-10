using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WindCommon.Common
{
    public class WinAPI
    {
        [DllImport("kernel32.dll")]
        public static extern int AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern int FreeConsole();

        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        public extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        public extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,   // handle to destination window
            int Msg,       // message 
            int wParam,      // first message parameter
            int lParam       // second message parameter
            );

        [DllImport("Kernel32.dll", EntryPoint = "GetConsoleWindow")]
        public static extern IntPtr GetConsoleWindow();


        //private IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        //private const int WM_SETICON = 0x0080;
    }
}

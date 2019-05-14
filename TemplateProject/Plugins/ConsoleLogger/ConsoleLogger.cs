using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindCommon.Common;

namespace WindCommon.Log.ConsoleLogger
{
    public class ConsoleLogger: System.Diagnostics.TraceListener, IDisposable
    {
        #region 私有变量
        private bool _state = false;

        //API常量参数
        private IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private const int WM_SETICON = 0x0080;
        private const int SC_CLOSE = 0xF060;
        #endregion

        public ConsoleLogger() { }

        /// <summary>
        /// 显示console输出环境
        /// </summary>
        /// <param name="title"></param>
        public void ShowConsole(string title = "SWTraceLoger")
        {
            WinAPI.AllocConsole();
            _state = true;
            Console.Title = title + " - " + System.Threading.Thread.CurrentThread.ManagedThreadId;
            ResetForegroundColor();

            AfterLoadRemoveCloseBtn(Console.Title);
        }

        /// <summary>
        /// 清空console内容
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }

        public void WriteLine(string message, ConsoleLogCategory category)
        {
            WriteLine(message, category.ToString());
        }

        #region 可根据日志级别来记录日志到文件，写入日志的优先级会先于console输出。

        public void WriteIf(Func<bool> func, string message)
        {
            if (func.Invoke())
                Write(message);
        }

        public void WriteLineIf(Func<bool> func, string message)
        {
            if (func.Invoke())
                WriteLine(message);
        }

        public void WriteLineIf(Func<bool> func, string message, ConsoleLogCategory category)
        {
            if (func.Invoke())
                WriteLine(message, category.ToString());
        }

        public void WriteLineIf(Func<bool> func, string message, string category)
        {
            if (func.Invoke())
                WriteLine(message, category);
        }
        #endregion

        #region 重写方法
        /// <summary>
        /// 关闭console输出环境
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _state = false;
                WinAPI.FreeConsole();
            }
        }

        public override void Write(string message)
        {
            if (!_state)
                return;
            try
            {
                Console.WriteLine(message);
            }
            catch { }
        }

        public override void WriteLine(string message)
        {
            if (!_state)
                return;
            try
            {
                Console.WriteLine(message);
            }
            catch { }
        }

        public override void WriteLine(string message, string category)
        {
            if (!_state)
                return;
            try
            {
                switch (category.ToLower())
                {
                    case "fail":
                    case "error":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "exception":
                    case "fatal":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case "info":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "warn":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "success":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "unknown":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    default:
                        break;
                }

                base.WriteLine(message, category);

                ResetForegroundColor();
            }
            catch { }
        }
        #endregion

        #region 私有方法

        private void ResetForegroundColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// 根据console的标题
        /// </summary>
        /// <param name="title"></param>
        private void AfterLoadRemoveCloseBtn(string title)
        {
            //线程等待，确保可以找到窗口
            System.Threading.Thread.Sleep(10);

            var icon = Resources.ico.Handle;
            //设置图标
            IntPtr hnd = WinAPI.GetConsoleWindow();
            if (hnd != INVALID_HANDLE_VALUE)
            {
                WinAPI.SendMessage(hnd, WM_SETICON, 0, (int)icon);     //发送更替图标的消息
            }

            ////根据控制台标题找控制台
            //int WINDOW_HANDLER = WinAPI.FindWindow(null, title);

            //找关闭按钮
            IntPtr CLOSE_MENU = WinAPI.GetSystemMenu(hnd, IntPtr.Zero);
            //关闭按钮禁用
            WinAPI.RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
        }
        #endregion
    }
}

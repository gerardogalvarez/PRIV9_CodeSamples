using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdiExtender
{
    public static class MDIExtender
    {
        #region DLLImport

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        #endregion

        #region Private Properties

            private static IntPtr MainWindowhWnd { get; set; }

        #endregion

        private static IntPtr FindWindow(string windowName, bool wait)
        {
            IntPtr hWnd = FindWindow(null, windowName);

            while (wait && hWnd == (IntPtr)0)
            {
                System.Threading.Thread.Sleep(500);
                hWnd = FindWindow(null, windowName);
            }

            return hWnd;
        }

        public static void SetMDIForm(Form oFrm, string WindowTitle)
        {
            IntPtr hw = new IntPtr();         

            if (MainWindowhWnd == IntPtr.Zero)
            {
                hw = FindWindow(WindowTitle, false);
            }
            else
            {
                hw = MainWindowhWnd;
            }

            hw = FindWindowEx(hw, IntPtr.Zero, "MDIClient", "");
            SetParent(oFrm.Handle, hw);
        }

    }
}
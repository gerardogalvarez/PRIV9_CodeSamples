using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win32 = Primavera.Core.Interop.Win32;

namespace MyHostWindow
{
    internal class HostManager
    {
        public Form Host { get; set; }

        public void ResizeHost(IntPtr ParentHwnd)
        {
  
            if (!Win32.Helper.IsNullOrZero(ParentHwnd))
            {
                Win32.Rect size = Win32.Helper.GetWindowSize(ParentHwnd);
                Host.Size = new System.Drawing.Size(size.Width, size.Height);
            }
        }

        public void SetHost(IntPtr ParentHwnd)
        {
            Host.ControlBox = false;
            Host.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Host.WindowState = FormWindowState.Normal;
            Host.StartPosition = FormStartPosition.Manual;
            Host.Location = new Point(0, 0);
            this.ResizeHost(ParentHwnd);

            // Attach host to parent window

            if (Win32.Helper.IsNullOrZero(ParentHwnd))
            {
                // Ignore it
            }
            else
            {
                Win32.NativeMethods.SetParent(Host.Handle, ParentHwnd);

                Host.Show();
            }
        }
    }
}

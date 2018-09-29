using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PainelContexto
{
    [ComVisible(true)]
    [Guid("D6D1639A-576D-4548-809C-29026CBE321C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IHostWindowEvent))]
    public class HostWindows : DisposableBase, IHostWindow 
    {
        public delegate void OnDrilDownDelegate(string codigo);
        private event OnDrilDownDelegate OnDrilDown;

        internal frmCliente frmCliente;
        internal HostManager hostManager;

        #region IHostWindow Members

        void IHostWindow.Mostra(IntPtr parentHwnd)
        {
            hostManager = new HostManager();
            frmCliente = new frmCliente();

            frmCliente.ExecutaDrillDown += FrmCliente_ExecutaDrillDown;

            this.hostManager.Host = frmCliente;
            this.hostManager.SetHost(parentHwnd);
        }

        private void FrmCliente_ExecutaDrillDown(object sender, EventArgs e)
        {
            OnDrilDown(sender.ToString());
        }

        void IHostWindow.Redimenciona(IntPtr parentHwnd)
        {
            this.hostManager.ResizeHost(parentHwnd);
        }

        #endregion

        #region IDisposable Members

        protected override void Dispose(bool disposing)
            {
                // Check to see if Dispose has already been called

                if (!this.Disposed)
                {
    
                }

                // Dispose on base class
                base.Dispose(disposing);
            }
        #endregion
    }
}

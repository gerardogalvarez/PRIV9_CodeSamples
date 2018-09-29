using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MyHostWindow
{
    [ComVisible(true)]
    [Guid("384A53F1-FEAC-4CFA-ABB8-D100A2E75198")]
    [ClassInterface(ClassInterfaceType.None)]
    public class HostWindows : DisposableBase, IHostWindow
    {

        internal frmCliente frmCliente;
        internal HostManager hostManager;
        internal dynamic cliente;

        #region Public
        public dynamic Entidade
        {
            set
            {
                cliente = value;
            }

            get
            {
                return cliente;
            }
        }
        #endregion

        #region IHostWindow Members

        void IHostWindow.Mostra(IntPtr parentHwnd)
        {
            hostManager = new HostManager();
            frmCliente = new frmCliente();

            this.hostManager.Host = frmCliente;
            this.hostManager.SetHost(parentHwnd);
        }

        void IHostWindow.Redimenciona(IntPtr parentHwnd)
        {
            this.hostManager.ResizeHost(parentHwnd);
        }



        void IHostWindow.Atualiza()
        {
            frmCliente.entidade = this.Entidade;
            frmCliente.CarregaEntidade();
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

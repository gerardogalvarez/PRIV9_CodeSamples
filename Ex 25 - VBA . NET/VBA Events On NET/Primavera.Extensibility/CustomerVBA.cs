using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Primavera.Extensibility
{
    [ComVisible(true)]
    [Guid("83F0D3D1-0B17-4453-852E-7CAA470B72AB")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CustomerVBA : DisposableBase, IHostWindow
    {
        #region internal

        internal dynamic cliente;

        #endregion     

        #region VBA Members

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

        public void AntesDeGravar(bool Cancel)
        {
            throw new NotImplementedException();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Primavera.Extensibility
{
    [ComVisible(true)]
    [Guid("83F0D3D1-0B17-4453-852E-7CAA470B72AB")]
    [ClassInterface(ClassInterfaceType.None)]
    public class CustomerVBA : DisposableBase, IHostWindow
    {
        #region internal

        internal dynamic cliente;

        internal dynamic articulo;

        internal Interop.StdPlatBS900.StdBSInterfPub Plataforma;

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

        public dynamic Articulo
        {
            set
            {
                articulo = value;
            }

            get
            {
                return articulo;
            }
        }

        public bool FichaArticulos_AntesDeEditar(string CodigoArticulo)
        {
            ExternalDLL.Proxy.Plataforma.Dialogos.MostraAviso("Aviso desde .NET!", Interop.StdPlatBS900.IconId.PRI_Critico, "Se ha ejecutado el FichaArticulos_AntesDeEditar (.NET)");
            return true;
        }

        public bool FichaArticulos_AntesDeGrabar(ref string strAviso)
        {
            if (string.IsNullOrEmpty(articulo.Descricao))
            {
                strAviso = "Artículos! La descripción es obligatoria.";
                return true;
            }
            else
            {
                strAviso = "";
                return false;
            }
        }

        public void FichaArticulos_DespuesDeEditar()
        {
            ExternalDLL.Proxy.Plataforma.Dialogos.MostraAviso("Aviso desde .NET!", Interop.StdPlatBS900.IconId.PRI_Critico, "Se ha ejecutado el FichaArticulos_DespuesDeEditar (.NET)");
        }

        public bool FichaClientes_AntesDeEditar(string Cliente)
        {
            if (ExternalDLL.Proxy.Plataforma.Dialogos.MostraPerguntaSimples(string.Format("AntesDeEditar. Cliente {0}", Cliente )))
                return true;
            else
                return false;
        }

        public bool FichaClientes_AntesDeGrabar(ref string strAviso)
        {
            if (string.IsNullOrEmpty(cliente.nome))
            {
                strAviso = "Clientes! El nombre es obligatorio";
                return true;
            }
            else
            {
                strAviso = "";
                return false;
            }
        }

        public void FichaClientes_DespuesDeEditar()
        {
            // nothing
        }

        #endregion


        // Pasamos el contexto a la DLL externa:
        public void setERPContext(Interop.StdPlatBS900.StdBSInterfPub erpPlat)
        {

            // La Plataforma:
            Plataforma = erpPlat;
        }

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

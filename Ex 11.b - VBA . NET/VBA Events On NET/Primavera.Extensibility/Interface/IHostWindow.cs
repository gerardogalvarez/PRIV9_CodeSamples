using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Primavera.Extensibility
{
    [ComVisible(true)]
    [Guid("93C76FF4-D300-4BD9-911C-6A7BEBD648B2")]
    public interface IHostWindow
    {

        #region Contexto

        void setERPContext(Interop.StdPlatBS900.StdBSInterfPub erpPlat);

        #endregion

        #region Clientes

        dynamic Entidade { set; }

        bool FichaClientes_AntesDeEditar(string Cliente);

        bool FichaClientes_AntesDeGrabar(ref string strAviso);

        void FichaClientes_DespuesDeEditar();

        #endregion

        #region Articulos

        dynamic Articulo { set; }

        bool FichaArticulos_AntesDeEditar(string CodigoArticulo);

        bool FichaArticulos_AntesDeGrabar(ref string strAviso);

        void FichaArticulos_DespuesDeEditar();

        #endregion



    }
}

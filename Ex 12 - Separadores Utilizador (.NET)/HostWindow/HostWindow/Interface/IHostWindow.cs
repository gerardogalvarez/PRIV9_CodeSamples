using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MyHostWindow
{
    [ComVisible(true)]
    [Guid("CB9CDA01-4581-4B9F-B77E-D7F5ACC7F4DD")]
    public interface IHostWindow
    {
        void Mostra(IntPtr parentHwnd);

        void Redimenciona(IntPtr parentHwnd);

        void Atualiza();

        dynamic Entidade { set; }

    }
}

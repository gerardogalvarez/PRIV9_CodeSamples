using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PainelContexto
{
    [ComVisible(true)]
    [Guid("24636BFD-CD80-4DA5-AFB7-1E15ACB98E85")]
    public interface IHostWindow
    {
        void Mostra(IntPtr parentHwnd);

        void Redimenciona(IntPtr parentHwnd);

    }
}

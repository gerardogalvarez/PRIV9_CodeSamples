using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PainelContexto
{
    [ComVisible(true)]
    [Guid("9AD3DDC8-FEA7-45A6-9D45-DE9A3AB84902")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHostWindowEvent
    {
        [DispId(1)]
        void OnDrilDown(string codigo);

    }
}

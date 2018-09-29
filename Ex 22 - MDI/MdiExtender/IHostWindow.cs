using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MdiExtender
{
    [ComVisible(true)]
    [Guid("F076E2B4-6FC8-4CA4-99B9-AB1AAF97E8CF")]
    public interface IHostWindow
    {
        void Mostra(string wTitle);

        void MostraPopUp();

    }
}

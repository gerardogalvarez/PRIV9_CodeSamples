using System;
using System.Runtime.InteropServices;

namespace PrimaveraAddin
{
    [ComVisible(true)]
    [Guid("B999B8C5-7D59-4EBC-AFFF-549A9A2C2D37")]
    public interface IPrimaveraAddin
    {
        void RegistarAddin(dynamic plataforma, dynamic Aplicacao);
    }
}

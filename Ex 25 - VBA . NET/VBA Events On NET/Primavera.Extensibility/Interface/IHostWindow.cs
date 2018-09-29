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
        void AntesDeGravar(Boolean Cancel);

        dynamic Entidade { set; }

    }
}

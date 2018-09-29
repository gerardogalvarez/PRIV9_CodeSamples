using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Notificacao
{
    [Guid(Guids.InterfaceId), ComVisible(true)]
    public interface IEventos
    {
        [DispId(1)]
        object Plataforma { [DispId(1)] set; }
    }
}

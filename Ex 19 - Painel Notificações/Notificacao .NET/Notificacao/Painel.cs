using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections;


namespace Notificacao
{
    [ComVisible(true)]
    [Guid(Guids.ClassId)]
    [ClassInterface(ClassInterfaceType.None)]
    public class Painel: IEventos
    {
        private object InterfPublico;


        #region IEventos Members

        object IEventos.Plataforma
        {
            set 
            {
                InterfPublico = value;

                dynamic plt = InterfPublico;
                plt.mdi.Notificacoes.MostraMensagem("Notificação", "Notificação da minha aplicação.net ", false);
                plt.Diagnosticos.Trace("Notificação da minha aplicação.net");
            
            }

        }

        #endregion
    }
}

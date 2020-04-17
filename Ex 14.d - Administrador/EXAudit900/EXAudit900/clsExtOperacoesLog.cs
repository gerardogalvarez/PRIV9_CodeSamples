using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.StdClasses900;

namespace EXTAudit900
{
    // Geral: Exemplo de implementação do audit para aplicações de parceiros.
    // Class: Implementar aqui as operações que estarão disponiveis para log no ERP.
    // Nome classe: cls<AbtAPL>OperacoesLog.
    //      AbtAPL: Abreviatura da aplicação e deve ter três caracteres.
    public class clsExtOperacoesLog:clsAplOperacoesLog 
    {
        #region _clsAplOperacoesLog Members
            
            clsOperacoesLog  _clsAplOperacoesLog.get_DaOperacoesLog(ref clsParamOpsLog objParametros)
            {
 	            clsOperacoesLog opLog;
    
                return null;
            }

        #endregion
    }
}

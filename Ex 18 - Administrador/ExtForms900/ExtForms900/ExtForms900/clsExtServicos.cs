using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.StdClasses900;

namespace ExtForms900
{
    public class clsExtServicos:clsAplServicos 
    {

        private string Utilizador { get; set; }
        private string PassWord { get; set; }

        #region _clsAplServicos Members

            void _clsAplServicos.AtribuiUtilizador(ref string strUtilizador, ref string strPassword)
            {
                this.Utilizador = strUtilizador;
                this.PassWord = strPassword;
            }

            void _clsAplServicos.Executa(ref short intIndex)
            {
            }

            void _clsAplServicos.Inicializa(ref Interop.StdBE900.EnumTipoPlataforma enuTipoPlataforma, ref string strEmpresa, ref string strInstalacao, ref clsLicenca objLic)
            {
            }

            void _clsAplServicos.Lista(ref clsParamAplServicos objParametros, ref Array strLista, ref VBA.Collection colLoc)
            {
                string[] servicos;
                servicos = new string[2];

                servicos[1] = "Assistente de criação de utilizadores";

                strLista = servicos;
            }

        #endregion
    }
}

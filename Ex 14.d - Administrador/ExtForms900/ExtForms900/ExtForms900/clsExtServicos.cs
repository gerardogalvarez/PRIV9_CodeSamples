using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                switch (intIndex)
                {
                case 1:
                    MessageBox.Show("Creación de usuarios...");
                    break;
                case 2:
                    MessageBox.Show("Recálculo de stocks...");
                    break;
            }
        }

            void _clsAplServicos.Inicializa(ref Interop.StdBE900.EnumTipoPlataforma enuTipoPlataforma, ref string strEmpresa, ref string strInstalacao, ref clsLicenca objLic)
            {
            }

            void _clsAplServicos.Lista(ref clsParamAplServicos objParametros, ref Array strLista, ref VBA.Collection colLoc)
            {
                string[] servicos;
                servicos = new string[3];

                servicos[1] = "Asistente de creación de usuarios";
                servicos[2] = "Recálculo de stocks";

                strLista = servicos;
            }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.StdClasses900;
using ExtForms900.UI;
using Microsoft.VisualBasic;

namespace ExtForms900
{
    public class clsExtParametrizacoes: clsAplParametrizacoes 
    {
        private string Utilizador { get; set; }
        private string PassWord { get; set; }

        #region _clsAplParametrizacoes Members

            void _clsAplParametrizacoes.AtribuiUtilizador(ref string strUtilizador, ref string strPassword)
            {
                this.Utilizador = strUtilizador;
                this.PassWord = strPassword;
            }

            void _clsAplParametrizacoes.Inicializa(ref Interop.StdBE900.EnumTipoPlataforma enuTipoPlataforma, ref string strEmpresa, ref string strInstalacao, ref clsLicenca objLic)
            {

            }

            void _clsAplParametrizacoes.Lista(ref clsParamAplParams objParametros, ref Array strLista, ref VBA.Collection colLoc)
            {
                string[] Parametros;

                Parametros = new string[3];

                Parametros[1] = "Parámetros Generales";
                Parametros[2] = "Logins";

                strLista = Parametros;
            }

            bool _clsAplParametrizacoes.ModuloDisponivelLocalizacao(ref Interop.StdBE900.EnumLocalizacaoSede enuLocSede)
            {
                //Obrigatório estar definido como verdadeiro.
                return true;
            }

            void _clsAplParametrizacoes.Mostra(ref short intIndex, ref short intModoOperacao, ref object objOwnerForm)
            {

                switch (intIndex)
                {
                    case 1:
                        frmGerais frmgerais = new frmGerais();
                        frmgerais.ShowDialog();
                        break;

                case 2:
                    frmLogins frmLogins = new frmLogins();
                    frmLogins.ShowDialog();
                    break;
            }

                Interaction.CallByName(objOwnerForm, "ActivaInterface", CallType.Method, true);
           
            }

            void _clsAplParametrizacoes.TiposExercicioSuportados(ref Interop.StdBE900.EnumTipoPlataforma enuTipoPlataforma, ref Array enuTiposExercicio)
            {
            }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.StdClasses900;
using ExtForms900.UI;
using Microsoft.VisualBasic;
using Interop.StdBE900;


namespace ExtForms900
{
    public class clsExtLoginsAssociados:clsAplLoginsAssociados


    {
        #region _clsAplLoginsAssociados Members

        bool _clsAplLoginsAssociados.EditaInfoAdicional(ref clsParamAplLoginsEdita objParams, ref object objLoginAssociado)
        {

            frmLogins objfrm = new frmLogins();
            objfrm.ShowDialog();

            Interaction.CallByName(objLoginAssociado, "Tag", CallType.Let, objfrm.Token);

            objfrm.Close();
            return true;

        }

        StdBETiposLoginAssociado _clsAplLoginsAssociados.ListaTipos(ref clsParamAplLoginsLista objParams)
        {

            StdBETiposLoginAssociado objRes = new StdBETiposLoginAssociado();

            dynamic objTipo = objRes;

            objTipo.InsereNovo("Ext", "Login_Apl_Externa", "Login Apl Externa", true, false);

            return objRes;

        }

        #endregion
    }
}

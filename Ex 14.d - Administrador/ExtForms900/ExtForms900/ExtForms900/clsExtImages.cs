using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interop.StdClasses900;
using stdole;

namespace ExtForms900
{
    public class clsExtImages: clsAplImages 
    {
        #region _clsAplImages Members

        stdole.StdPicture _clsAplImages.get_Icon(ref Interop.StdBE900.EnumTipoPlataforma TipoPlataforma)
        {

           //var pic = (stdole.StdPicture) Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPicture(Resource.operacoes.ToBitmap());

            var pic = ExtForms900.IconConverter.ImageToIPicture(Resource.operacoes.ToBitmap());
            return (stdole.StdPicture)pic;
        }

        #endregion
    }

    public class IconConverter : System.Windows.Forms.AxHost
    { 
        private IconConverter():base(string.Empty)
        {
        }

        public static stdole.IPictureDisp ImageToIPicture(System.Drawing.Image image)
        {

            return (stdole.IPictureDisp)IconConverter.GetIPictureDispFromPicture(image);
        
        }
    }

}

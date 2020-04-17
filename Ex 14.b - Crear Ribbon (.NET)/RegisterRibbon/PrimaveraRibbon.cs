using Interop.StdPlatBE900;
using Interop.StdPlatBS900;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PrimaveraAddin
{
    [ComVisible(true)]
    [Guid("48559BFF-1559-4B1D-988D-DE038ACCA39F")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PrimaveraRibbon : IPrimaveraAddin
    {
#region Variables privadas

        private StdBSPRibbon RibbonEvents;

#endregion

#region Propiedades privadas

        private dynamic Tab { get; set; }

        private dynamic Group { get; set; }

        // private StdBSInterfPub PlataformaPrimavera { get; set; }
        private dynamic PlataformaPrimavera { get; set; }

        private dynamic Apl { get; set; }

#endregion

#region Eventos 

        private void RibbonEvents_Executa(ref string Id, ref string Comando)
        {
            switch (Id)
            {
                case RibbonConstants.CIDBOTAO1:

                    Apl.Utilizad
                    // Validamos si el usuario tiene acceso a la operación:
                    if (Apl.Utilizador.AcedeOperacao("EXT", "mnuOperacao1"))
                    {

                        // Notificación:
                        PlataformaPrimavera.MDI.Notificacoes.MostraInformacao("Notificación", "Notificación del panel de información.");

                        // Trace en la ventana de diagnósticos:
                        PlataformaPrimavera.Diagnosticos.Trace("Usuario pulsó en la opción 1.");

                        // Trace a ficheiro:
                        PlataformaPrimavera.Diagnosticos.TraceFicheiro(@"C:\erp.log", "Usuario pulsó en la opción 1.");

                        // Mensaje por pantalla:
                        PlataformaPrimavera.Dialogos.MostraAviso("Usuario pulsó la opción 1");
                    }
                    else
                    {
                        // Mostramos el mensaje de error en caso de no tener permisos:
                        PlataformaPrimavera.Dialogos.MostraAviso("El usuario no tiene permisos para acceder a esta operación.", IconId.PRI_Critico);
                    }

                    break;

                case RibbonConstants.CIDBOTAO2:

                    // Validamos si el usuario tiene acceso a la operación:
                    if (Apl.Utilizador.AcedeOperacao("EXT", "mnuOperacao2"))
                    {

                        // Mensaje por pantalla:
                        PlataformaPrimavera.Dialogos.MostraAviso("Usuario pulsó la opción 2");
                    }
                    else
                    {
                        // Mostramos el mensaje de error en caso de no tener permisos:
                        PlataformaPrimavera.Dialogos.MostraAviso("El usuario no tiene permisos para acceder a esta operación.");
                    }

                    break;
            }

        }

#endregion

#region Metodos Publicos

        public void RegistarAddin(dynamic plataforma, dynamic Aplicacao)
        {
            PlataformaPrimavera = plataforma;

            Apl = Aplicacao;

            RibbonEvents = plataforma.Ribbon;
            RibbonEvents.Executa += RibbonEvents_Executa;

            CriarTab();
            CriarGroup();
            CriarBotao16();
            CriarBotao16_2();
        }

#endregion

#region Metodos Privados

        private void CriarTab()
        {
            Tab = PlataformaPrimavera.Ribbon.CriaRibbonTabERP("PRIMAVERA .NET", RibbonConstants.cIDTAB, 10);
        }

        private void CriarGroup()
        {
            Group = PlataformaPrimavera.Ribbon.CriaRibbonGroup(Tab, "Configuraciones", RibbonConstants.CIDGROUP);
        }

        private void CriarBotao16()
        {
            StdBEBotaoRibbon botao = new StdBEBotaoRibbon();
            StdBEBotoesRibbon botoes = new StdBEBotoesRibbon();

            botao.set_Descricao("Seguridad");
            botao.set_Icon32(false);
            botao.set_Id(RibbonConstants.CIDBOTAO1);

            var pic = IconConverter.ImageToIPicture(Image.FromFile(@"C:\IconsStandard\16x16\operacoes.ico")); // Resource.operacoes.ToBitmap())
            botao.set_Icon((stdole.StdPicture)pic);

            botoes.Insere(botao);

            PlataformaPrimavera.Ribbon.AdicionaBotoes(botoes, "Botoes", RibbonConstants.CIDGROUP);
        }

        private void CriarBotao16_2()
        {
            StdBEBotaoRibbon botao = new StdBEBotaoRibbon();
            StdBEBotoesRibbon botoes = new StdBEBotoesRibbon();

            botao.set_Descricao("Aviso");
            botao.set_Icon32(false);
            botao.set_Id(RibbonConstants.CIDBOTAO2);

            //var pic = IconConverter.ImageToIPicture(Image.FromFile(@"C:\IconsStandard\16x16\Feramentas.ico"));
            var pic = IconConverter.ImageToIPicture(Resource.operacoes.ToBitmap());
            botao.set_Icon((stdole.StdPicture)pic);

            botoes.Insere(botao);

            PlataformaPrimavera.Ribbon.AdicionaBotoes(botoes, "Botoes", RibbonConstants.CIDGROUP);
        }

        #endregion
    }
}

public class IconConverter : System.Windows.Forms.AxHost
{
    private IconConverter() : base(string.Empty)
    {
    }

    public static stdole.IPictureDisp ImageToIPicture(System.Drawing.Image image)
    {

        return (stdole.IPictureDisp)IconConverter.GetIPictureDispFromPicture(image);

    }
}
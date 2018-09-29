using Interop.StdPlatBE900;
using Interop.StdPlatBS900;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PrimaveraAddin
{
    [ComVisible(true)]
    [Guid("48559BFF-1559-4B1D-988D-DE038ACCA39F")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PrimaveraRibbon : IPrimaveraAddin
    {
        #region Variaveis privadas

        private StdBSPRibbon RibbonEvents;

        #endregion

        #region Propriedades privadas
        private dynamic tab { get; set; }

        private dynamic group { get; set; }

        private dynamic plataformaPrimavera { get; set; }

        private dynamic apl { get; set; }

        #endregion

        #region Eventos 
        private void RibbonEvents_Executa(ref string Id, ref string Comando)
        {
            // Notificação.
            plataformaPrimavera.MDI.Notificacoes.MostraInformacao ("Notificação", "Notificação do painel de informação.");

            // Trace.
            plataformaPrimavera.Diagnosticos.Trace("Utilizador carregou na opção 1.");

            // Trace ficheiro.
            plataformaPrimavera.Diagnosticos.TraceFicheiro(@"C:\erp.log", "Utilizador carregou na opção 1.");


            if (apl.Utilizador.AcedeOperacao("EXT", "mnuOperacao1"))
            {
                MessageBox.Show("Com permisssão.");
     
            }
            else
            {
                MessageBox.Show("Sem permisssão.");
            }
        }

        #endregion

        #region Metodos Publicos
        public void RegistarAddin(dynamic plataforma, dynamic Aplicacao)
        {
            plataformaPrimavera = plataforma;

            apl = Aplicacao;

            RibbonEvents = plataforma.Ribbon;
            RibbonEvents.Executa += RibbonEvents_Executa;

            CriarTab();
            CriarGroup();
            CriarBotao16();
        }

        #endregion

        #region Metodos Privados

        private void CriarTab()
        {
            tab = plataformaPrimavera.Ribbon.CriaRibbonTabERP("PRIMAVERA PT", RibbonConstants.cIDTAB, 10);
        }

        private void CriarGroup()
        {
            group = plataformaPrimavera.Ribbon.CriaRibbonGroup(tab, "Configurações", RibbonConstants.CIDGROUP);
        }

        private void CriarBotao16()
        {
            StdBEBotaoRibbon botao = new StdBEBotaoRibbon();
            StdBEBotoesRibbon botoes = new StdBEBotoesRibbon();

            botao.set_Descricao("Segurança");
            botao.set_Icon32(false);
            botao.set_Id(RibbonConstants.CIDBOTAO1);

            var pic = IconConverter.ImageToIPicture(Resource.operacoes.ToBitmap());
            botao.set_Icon((stdole.StdPicture)pic);

            botoes.Insere(botao);

            plataformaPrimavera.Ribbon.AdicionaBotoes(botoes, "Botoes", RibbonConstants.CIDGROUP);
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
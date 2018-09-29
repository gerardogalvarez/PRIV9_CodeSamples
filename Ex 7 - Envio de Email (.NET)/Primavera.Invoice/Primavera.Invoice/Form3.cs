using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interop.GcpBE900;
using Primavera.Invoice.Engine;
using Interop.StdBE900;

namespace Primavera.Invoice
{
    public partial class frmInvoice : Form
    {
        public frmInvoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (PriEngine.Engine.Comercial.Artigos.Existe(txtArtigo.Text))
            {
                string[] row = { txtArtigo.Text, txtQtd.Value.ToString() };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
            else
            {
                MessageBox.Show("O artigo indicado não existe.");

                txtArtigo.Clear();
                txtArtigo.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGravar_Click(object sender, EventArgs e)
        {
          
            GcpBEDocumentoVenda invoice = new GcpBEDocumentoVenda();
            string strAvisos = string.Empty;

            // Prepara dados cabeçalho
            invoice.set_Tipodoc(txtTipoDoc.Text);
            invoice.set_Entidade(txtEntidade.Text);

            invoice.set_TipoEntidade("C");
            invoice.set_Serie(txtSerie.Text);

            try
            {
                if (checkBox1.Checked)
                {
                    // Prenche informação relacionada com o documento e Entidade (Modo pagamento, Moeda, Expedição, condições pagamento...)
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(invoice);
                }

                if (listView1.Items.Count > 0)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        //Adiciona as linhas ao documento
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(invoice, item.SubItems[0].Text, Convert.ToDouble(item.SubItems[1].Text), "a1", "a1", 2000);
                    }

                    if (txtobs.Text.Length > 0)
                    {
                        //Adiciona uma Linhas de comentarios
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinhaEspecial(invoice, vdTipoLinhaEspecial.vdLinha_Comentario, 0, txtobs.Text);
                    }

                }
                else
                {
                    MessageBox.Show("Não existem artigos.");
                }

                PriEngine.Engine.Comercial.Vendas.CalculaValoresTotais(invoice);

                PriEngine.Engine.Comercial.Vendas.Actualiza(invoice, ref strAvisos);

                if (strAvisos.Length > 0)
                {
                    MessageBox.Show("Erro ao gravar o documento. \n" + strAvisos);
                }
                else
                {
                    MessageBox.Show("Documento Gravado");


                    PriEngine.Engine.Comercial.Vendas.ImprimeDocumento(invoice.get_Tipodoc(),invoice.get_Serie(),  invoice.get_NumDoc(), "000", 0, null, false, @"C:\temp\fatura.pdf");

                    PriEngine.Engine.Comercial.LigacaoCBL.IntegraDocumentoLogCBL("V", invoice.get_Tipodoc(), invoice.get_Serie(), invoice.get_NumDoc(), "000");

                    StringBuilder strAssunto = new StringBuilder();

                    strAssunto.Append("Registada Venda ");

                    strAssunto.Append(invoice.get_Tipodoc());
                    strAssunto.Append(invoice.get_NumDoc());

                    PriEngine.Platform.Mail.Inicializa();
                    
                    PriEngine.Platform.Mail.EnviaMailEx("elevation.demo1@gmail.com", null, null, strAssunto.ToString(), null, @"c:\temp\fatura.pdf", false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel gravar o documento. \n" + ex.Message);
            }
            finally
            {

            }


        }

        /// <summary>
        /// valida tipo doc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTipoDoc_Validating(object sender, CancelEventArgs e)
        {
            if (!PriEngine.Engine.Comercial.TabVendas.Existe(txtTipoDoc.Text) & (txtTipoDoc.Text.Length > 0))
            {
                MessageBox.Show("O tipo documento indicado não existe.");
                txtTipoDoc.Clear();
                txtTipoDoc.Focus();
            }
            else
            {
                txtDescricao.Text  = (string) PriEngine.Engine.Comercial.TabVendas.DaValorAtributo(txtTipoDoc.Text, "Descricao");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEntidade_Validating(object sender, CancelEventArgs e)
        {
            if (!PriEngine.Engine.Comercial.Clientes.Existe(txtEntidade.Text) & (txtEntidade.Text.Length > 0))
            {
                MessageBox.Show("A entidade indicada não existe.");
                txtEntidade.Clear();
                txtEntidade.Focus();
            }
            else
            {
                object[] camposBD = new object[2] { "Nome", "NumContrib" };

                dynamic campos = PriEngine.Engine.Comercial.Clientes.DaValorAtributos(txtEntidade.Text, ref camposBD);

                txtNome.Text = campos[1].valor;
                txtNif.Text = campos[2].valor;

            }
        }

        /// <summary>
        /// Valida Serie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSerie_Validating(object sender, CancelEventArgs e)
        {
            if (!PriEngine.Engine.Comercial.Series.Existe("V", txtTipoDoc.Text, txtSerie.Text) & (txtSerie.Text.Length > 0))
            {
                MessageBox.Show("A serie indicada não está definida para o tipo documento.");
                txtSerie.Clear();
                txtSerie.Focus();
            }
        }

        private void txtEntidade_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

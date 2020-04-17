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
                MessageBox.Show("The selected item don't exist.");

                txtArtigo.Clear();
                txtArtigo.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            GcpBEDocumentoVenda invoice = new GcpBEDocumentoVenda();
            string strAvisos = string.Empty;

            invoice.set_Tipodoc(txtTipoDoc.Text);
            invoice.set_Entidade(txtEntidade.Text);
            invoice.set_TipoEntidade("C");
            invoice.set_Serie(txtSerie.Text);
            // Set as draft.
            //invoice.set_Rascunho(true);

            try
            {
                if (checkBox1.Checked)
                {
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(invoice);
                }

                if (listView1.Items.Count > 0)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        GcpBELinhasDocumentoVenda linhas = new GcpBELinhasDocumentoVenda();

                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(invoice, item.SubItems[0].Text, Convert.ToDouble(item.SubItems[1].Text));
                        linhas = invoice.get_Linhas();
                        linhas[1].get_CamposUtil().set_Item("CDU_LinVar1", "12");
                        linhas[1].set_Desconto1(10);
                    }

                    if (txtobs.Text.Length > 0)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinhaEspecial(invoice, vdTipoLinhaEspecial.vdLinha_Comentario, 0, txtobs.Text);
                    }

                }
                else
                {
                    MessageBox.Show("There are no itens.");
                }

                PriEngine.Engine.Comercial.Vendas.CalculaValoresTotais(invoice);

                // Save the document as draft.
                //PriEngine.Engine.Comercial.Vendas.ActualizaRascunho(invoice, ref strAvisos);

                PriEngine.Engine.Comercial.Vendas.Actualiza(invoice, ref strAvisos);

                if(!PriEngine.Engine.Comercial.Vendas.ImprimeDocumento(invoice.get_Tipodoc(), invoice.get_Serie(), invoice.get_NumDoc(), "000", 1, "", true, "c:\\temp\\demo.pdf"))
                {
                    MessageBox.Show(string.Format("Error printing document {0}.\n Message: {1}", GetDocName(invoice), strAvisos));
                }
                else if (strAvisos.Length > 0)
                {
                    MessageBox.Show(string.Format("Alert printing document {0}.\n Warnings: {1}", GetDocName(invoice), strAvisos));
                }
                else
                {
                    MessageBox.Show(string.Format("Document {0} printed with success.", GetDocName(invoice)));
                }

                // PriEngine.Engine.Comercial.LigacaoCBL.IntegraDocumentoLogCBLEX("V", invoice.get_Tipodoc(), invoice.get_Serie(), invoice.get_NumDoc(), "000", Avisos: strAvisos)

                strAvisos = string.Empty;

                PriEngine.Engine.Comercial.LigacaoCBL.IntegraDocumentoLogCBL("V", invoice.get_Tipodoc(), invoice.get_Serie(), invoice.get_NumDoc(), "000", Avisos: strAvisos);

                if (!string.IsNullOrEmpty( strAvisos))
                {
                    MessageBox.Show("Document linked to CBL. \n");
                }
                else
                {
                    MessageBox.Show("Error linking document CBL!!! \n" + strAvisos);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to save document {0}.\n Message: {1}.", GetDocName(invoice), ex.Message));
            }
            finally
            {

            }
        }

        private object GetDocName(GcpBEDocumentoVenda invoice)
        {
            return String.Format("{0} {1}/{2}", invoice.get_Tipodoc(), invoice.get_Serie(), invoice.get_NumDoc().ToString());
        }

        /// <summary>
        /// Validate document type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTipoDoc_Validating(object sender, CancelEventArgs e)
        {
            if (!PriEngine.Engine.Comercial.TabVendas.Existe(txtTipoDoc.Text) & (txtTipoDoc.Text.Length > 0))
            {
                MessageBox.Show("The document type don't exist.");
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
                MessageBox.Show("The selected entity don't exist");
                txtEntidade.Clear();
                txtEntidade.Focus();
            }
            else
            {
                object[] camposBD = new object[2] { "Nome", "NumContrib" };

                dynamic campos = PriEngine.Engine.Comercial.Clientes.DaValorAtributos(txtEntidade.Text, ref camposBD);

                if (campos != null)
                {
                    txtNome.Text = campos[1].valor;
                    txtNif.Text = campos[2].valor;
                }
            }
        }

        /// <summary>
        /// Validate document Series.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSerie_Validating(object sender, CancelEventArgs e)
        {
            if (!PriEngine.Engine.Comercial.Series.Existe("V", txtTipoDoc.Text, txtSerie.Text) & (txtSerie.Text.Length > 0))
            {
                MessageBox.Show("The series is not defined for the selected document type.");
                txtSerie.Clear();
                txtSerie.Focus();
            }
        }
    }
}

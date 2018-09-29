using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Primavera.Invoice.Engine;
using Interop.StdBE900;

namespace Primavera.Invoice
{
    public partial class Form1 : Form
    {

        #region "Demo Events"

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button3_Click(object sender, EventArgs e)
            {
                frmInvoice createInvoice = new frmInvoice();
            
                createInvoice.Show();

            }

            private void cmdCustomer_Click(object sender, EventArgs e)
            {
                frmCustomerList CustomerList = new frmCustomerList();

                CustomerList.Show();
            }

        #endregion

        #region "Login Events"
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void cmdLogin_Click(object sender, EventArgs e)
            {
                try
                {
                    if (PriEngine.InitializeCompany(txtempresa.Text, txtuser.Text, txtpass.Text))
                    {
                        StringBuilder status = new StringBuilder();

                        status.Append("Company: " + PriEngine.Platform.Contexto.Empresa.CodEmp + "\n");
                        status.Append("Company Name: " + PriEngine.Platform.Contexto.Empresa.IDNome + "\n");
                        status.Append("Currency: " + PriEngine.Platform.Contexto.Empresa.MoedaBase + "\n");

                        lblStatus.Text = status.ToString();

                        groupBox1.Enabled = true;

                    }
                    else
                    {
                        lblStatus.Text = "Cannot acess to primavera. Check your configuration.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PriEngine.Termina();
                }
        }
        #endregion

        #region "Form Events"

            /// <summary>
            /// 
            /// </summary>
            public Form1()
            {
                InitializeComponent();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Form1_FormClosed(object sender, FormClosedEventArgs e)
            {
                PriEngine.Termina();
                this.Dispose();
            }

        #endregion
    }
}

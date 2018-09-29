using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrimaveraIntegration.Class;
using Interop.StdBE900;

namespace PrimaveraIntegration.UI
{
    public partial class Form1 : Form
    {

        #region "Demo Events"

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button1_Click(object sender, EventArgs e)
            {
                FrmCustomer custumer = new FrmCustomer();

                custumer.Show();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button2_Click(object sender, EventArgs e)
            {
                frmCustomerList custumerList = new frmCustomerList();

                custumerList.Show();

            }

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

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void cmdPurchaseInvoice_Click(object sender, EventArgs e)
            {
                frmPurchaseInvoice createInvoice = new frmPurchaseInvoice();

                createInvoice.Show();
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

                if (PriEngine.InitializeCompany(txtempresa.Text , txtuser.Text, txtpass.Text))
                {
                    object u = PriEngine.Platform.Contexto.Utilizador.get_objUtilizador();
                
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
                this.Dispose();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button4_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Demo unavailable.");

                frmActivity Activity = new frmActivity();
                Activity.Show();

            }
        #endregion

    }
}

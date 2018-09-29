using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using PrimaveraIntegration.Class;
using Interop.GcpBE900;
using Interop.StdBE900;

namespace PrimaveraIntegration.UI
{
    public partial class frmCustomerList : Form
    {
        public frmCustomerList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolrefresh_Click(object sender, EventArgs e)
        {
            StdBELista MyRs = new StdBELista();

            try
            {
                MyRs = PriEngine.Engine.Comercial.Clientes.LstClientes();
                
                OleDbDataAdapter myDA = new OleDbDataAdapter();
                DataSet myDS = new DataSet("MyTable");
                myDA.Fill(myDS, MyRs.get_DataSet(), "MyTable");

                dataGridView1.DataSource = myDS;
                dataGridView1.DataMember = "MyTAble";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro loading costumer...");
            }
            finally
            {
                MyRs = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmCustomerList_Load(object sender, EventArgs e)
        {

        }
    }
}

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
using Primavera.Invoice.Engine;
using Interop.GcpBE900;
using Interop.StdBE900;
using System.Data.SqlClient;

namespace Primavera.Invoice
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
                //MyRs = PriEngine.Engine.Comercial.Clientes.LstClientes();
                
                //OleDbDataAdapter myDA = new OleDbDataAdapter();
                //DataSet myDS = new DataSet("MyTable");
                //myDA.Fill(myDS, MyRs.get_DataSet(), "MyTable");

                //dataGridView1.DataSource = myDS;
                //ataGridView1.DataMember = "MyTAble";

                dynamic plt = PriEngine.Platform;
                dynamic mtr = PriEngine.Engine;

                string connectionString = plt.BaseDados.DaConnectionStringNET(plt.BaseDados.DaNomeBDdaEmpresa(mtr.Contexto.CodEmp), "Default");

                string query= "SELECT Nome, NumContrib FROM clientes";

                SqlConnection con= new SqlConnection(connectionString);

                ////SqlCommand command = new SqlCommand();
                ////command.CommandType = CommandType.Text;
                ////command.Connection = con;
                ////command.CommandText= "INSERT INTO  Nome, NumContrib FROM clientes";

                ////command.ExecuteReader();

                SqlDataAdapter da= new SqlDataAdapter(query, con);

                SqlCommandBuilder cb= new SqlCommandBuilder(da);
                DataSet ds= new DataSet();

                da.Fill(ds);
                dataGridView1.DataSource= ds.Tables[0];

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

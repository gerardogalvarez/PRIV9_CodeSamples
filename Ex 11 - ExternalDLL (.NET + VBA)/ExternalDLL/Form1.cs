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

namespace ExternalDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region --- Usando la Plataforma PRIMAVERA a través de nuestro Proxy --- 

        private void btnF4Funcionario_Click(object sender, EventArgs e)
        {
            // A través de nuestro "Proxy", donde habíamos pasado el contexto de la Plataforma...
            //  ... usamos listas SQL:
            txtFuncionario.Text = (string)Proxy.Plataforma.Listas.GetF4SQL("Trabajadores", "SELECT codigo, nome FROM Funcionarios WITH(NOLOCK)", "codigo");
        }

        private void txtFuncionario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && !e.Shift)
            {
                txtFuncionario.Text = (string)Proxy.Plataforma.Listas.GetF4SQL("Trabajadores", "SELECT codigo, nome FROM Funcionarios WITH(NOLOCK)", "codigo");
            }
        }

        #endregion

        #region --- Usando los motores PRIMAVERA a través de nuestro Proxy --- 

        private void txtFuncionario_Validated(object sender, EventArgs e)
        {
            if (txtFuncionario.Text != "")
            {
                txtNome.Text = Proxy.BSO.RecursosHumanos.Funcionarios.DaValorAtributo(txtFuncionario.Text, "Nome").ToString();
            }
        }

        #endregion

        #region --- Usando la Plataforma y motores PRIMAVERA a través de nuestro Proxy --- 

        private void button1_Click(object sender, EventArgs e)
        {
            string nome;

            // A través de nuestro "Proxy", donde habíamos pasado el contexto de los motores...
            //  ... accedemos a cualquier módulo:
            nome = Proxy.BSO.Comercial.Clientes.DaNome("SOFRIO");

            // A través de nuestro "Proxy", donde habíamos pasado el contexto de la Plataforma...
            //  ... mostramos diálogos PRIMAVERA:
            Proxy.Plataforma.Dialogos.MostraAviso(nome);
        }

        #endregion

        #region --- Usando el ConnectionString a través de nuestro Proxy --- 

        private void button2_Click(object sender, EventArgs e)
        {

            string sql = "SELECT cliente, nome, Fac_Local, NumContrib, Fac_tel FROM Clientes WITH(NOLOCK)";

            // A través de nuestro "Proxy", donde habíamos pasado la ConnectionString...
            //  ... ejecutamos SQL puro y duro:
            SqlDataAdapter objReader = new SqlDataAdapter(sql, Proxy.SQLConnection);
            DataTable datatable = new DataTable();
            objReader.Fill(datatable);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = datatable;
        }

        #endregion
    }
}

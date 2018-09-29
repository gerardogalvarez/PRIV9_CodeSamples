using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyHostWindow
{
    public partial class frmCliente : Form
    {
        public dynamic entidade { get; set; }

        public void CarregaEntidade()
        {
            txtMorada.Text = entidade.Morada;
            txtNome.Text = entidade.cliente;

        }

        public frmCliente()
        {
            InitializeComponent();
        }

        private void frmtest_Load(object sender, EventArgs e)
        {

        }
    }
}

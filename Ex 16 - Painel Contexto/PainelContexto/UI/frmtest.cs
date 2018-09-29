using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PainelContexto
{
    public partial class frmCliente : Form
    {
        public event EventHandler ExecutaDrillDown;


        protected virtual void OnExecutaDrillDown(EventArgs e, string codigo)
        {
            ExecutaDrillDown(codigo, e);
        }

        public frmCliente()
        {
            InitializeComponent();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnExecutaDrillDown(new EventArgs(), "sofrio");
        }
    }
}

﻿using System;
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
using Interop.GcpBE900;
using Primavera.Platform.NetServices;

namespace Primavera.Invoice
{
    public partial class Form1 : Form
    {

        #region "Demo Events"


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

        #endregion

            private void cmdSerialize_Click(object sender, EventArgs e)
            {

                NetServices serialize = new NetServices();
                GcpBEDocumentoVenda fatura = new GcpBEDocumentoVenda();

                fatura = PriEngine.Engine.Comercial.Vendas.EditaID(txtDocId.Text);
                txtXml.Text = serialize.ErpReflectionService.SerializeBE(fatura, "Gcp", txtuser.Text, txtempresa.Text);

            }


    }
}

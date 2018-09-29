using Interop.ErpBS900;
using Interop.GcpBE900;
using Interop.StdBE900;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BackgroundWorkerInvoice
{
    public partial class Form1 : Form
    {
        #region Propriedades

        private BackgroundWorker bw;
        ErpBS MotorLE;
        EnumTipoPlataforma objTipoPlataforma;
        StdBETransaccao objStdTransac;

        #endregion

        #region Form methods
        public Form1()
        {
            InitializeComponent();
            Register_BackgroundWorker();
        }

        private void cmdCriaFatura_Click(object sender, EventArgs e)
        {
            if (!this.bw.IsBusy)
            {
                MotorLE = new ErpBS();
                objTipoPlataforma = new EnumTipoPlataforma();
                objStdTransac = new StdBETransaccao();

                bool blnModoPrimario = true;
                objTipoPlataforma = EnumTipoPlataforma.tpEmpresarial;

                try
                {
                    this.MotorLE.AbreEmpresaTrabalho(EnumTipoPlataforma.tpEmpresarial, txtEmpresa.Text
                                                , txtUtilizador.Text, txtPassword.Text
                                                , ref objStdTransac
                                                , "Default"
                                                , ref blnModoPrimario);

                    this.bw.RunWorkerAsync();
                    this.cmdCriaFatura.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show ("Erro ao abrir o motor. \n" + ex.Message.ToString());
                }
            }
        }

        #endregion

        #region Background Worker
        private void Register_BackgroundWorker()
        {
            this.bw = new BackgroundWorker();

            this.bw.DoWork += new DoWorkEventHandler(Bw_DoWork);
            this.bw.ProgressChanged += new ProgressChangedEventHandler(Bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);
            this.bw.WorkerReportsProgress = true;
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
                this.cmdCriaFatura.Enabled = true;
            }
            else
            {
                this.lblStatus.Text = "Status: " + e.Result.ToString();

                // Fecha o motor.
                this.MotorLE.FechaEmpresaTrabalho();

                // Liberta os recursos com todos
                ReleaseCom(MotorLE);
            }

        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lblStatus.Text = e.ProgressPercentage.ToString() + " faturas criadas.";
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            for (int i = 1; i <= 10; ++i)
            {
                // report your progres
                worker.ReportProgress(i);

                CriarFatura();
            }
            e.Result = "OK";
        }

        #endregion

        #region Metodos privados
        private void CriarFatura()
        {
            string avisos = string.Empty;

            try
            {
                GcpBEDocumentoVenda docVenda = new GcpBEDocumentoVenda();

                docVenda.set_Tipodoc(txtTipoDoc.Text);
                docVenda.set_Serie(txtSerie.Text);
                docVenda.set_TipoEntidade("C");
                docVenda.set_Entidade("Sofrio");

                MotorLE.Comercial.Vendas.PreencheDadosRelacionados(docVenda);

                MotorLE.Comercial.Vendas.AdicionaLinha(docVenda, "A0001", 2);

                MotorLE.Comercial.Vendas.Actualiza(docVenda, avisos);

            }
            catch
            {
                StringBuilder erro = new StringBuilder();

                erro.Append("Erro a gerar o documento \n");
                erro.Append(avisos);

                throw new Exception(erro.ToString());
            }
        }

        #endregion

        #region Liberta Com Objects
        private static void ReleaseCom(dynamic ObjCom)
        {
            try
            {
                if (ObjCom != null)
                {
                    while (Marshal.ReleaseComObject(ObjCom) > 0)
                    {
                        // release one by one
                    };

                    ObjCom = null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao finalizar o motor.");
            }
        }

        #endregion
    }
}

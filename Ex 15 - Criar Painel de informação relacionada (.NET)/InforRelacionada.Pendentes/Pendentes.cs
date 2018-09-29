using System.Data;
using System.Text;
using System.Windows.Forms;
using Interop.StdPlatBE900;
using Interop.StdBE900;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Primavera.InfoRelacionada
{
    public partial class Pendentes : UserControl,IStdBEInfRelacionada
    {
        private StdBECategoryInfo m_objBECategoryInfo;
        private dynamic m_objFormContexto;
        private dynamic m_objPlataforma;
        private dynamic m_objMotorAplicacao;

        public Pendentes()
        {
            InitializeComponent();
        }

        #region "Propriedades Privadas"

            private string ValorChave { get; set; }

        #endregion

        #region "Metodos Privados"

        void CarregaGrelha()
        {

            string connectionString = m_objPlataforma.BaseDados.DaConnectionStringNET(m_objPlataforma.BaseDados.DaNomeBDdaEmpresa(m_objMotorAplicacao.Contexto.CodEmp), "Default");
            
            StringBuilder sql = new StringBuilder();
            string query = string.Empty; 

            sql.Append("Select TipoDoc,TipoConta,sum(ValorPendente) Total ,sum(Valortotal)as Pendente FROM");
            sql.Append(" Pendentes where entidade='@1@'");
            sql.Append(" GROUP BY TipoConta,TipoDoc");

            query = sql.ToString();
            query = query.Replace("@1@", this.ValorChave);

            try
            {
                dataGridPendentes.ReadOnly = true;

                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                SqlCommandBuilder comBuilder = new SqlCommandBuilder(dataAdapter);
                DataSet ds = new DataSet();

                dataAdapter.Fill(ds);
                dataGridPendentes.DataSource = ds.Tables[0];
            }
            catch 
            {
  
            }
        
        }

        #endregion

        #region _IStdBEInfRelacionada Members

            void _IStdBEInfRelacionada.AdicionaChave(string Nome, object Valor)
            {

                StdBECamposChave BECamposChave = new StdBECamposChave();

                BECamposChave.AddCampoChave(Nome, Valor);

                dynamic campo = BECamposChave.CamposChave;

                this.ValorChave = (string)campo[1].Valor;
        
            }

            void _IStdBEInfRelacionada.Atualiza()
            {
                CarregaGrelha();
            }

            void _IStdBEInfRelacionada.Limpa()
            {
                dataGridPendentes.DataSource = null;
            }

            void _IStdBEInfRelacionada.set_FormContexto(ref object value)
            {

            // Nome das propriedades nos formulários
            // Clientes - Cliente
            // Fornecedores - Fornecedor
            // Vendas - m_objDocVenda
            // Compras - ObjDocCompra

            dynamic oCliente;

            // Obtém o BE associado ao contexto.
            oCliente = (Interaction.CallByName(value, "Cliente", CallType.Get));
            }

            void _IStdBEInfRelacionada.set_Plataforma(ref object value)
            {
                m_objPlataforma = value;
            }

            void _IStdBEInfRelacionada.set_MotorAplicacao(ref object value)
            {
                m_objMotorAplicacao = value ;
            }

            StdBECategoryInfo _IStdBEInfRelacionada.get_BECategoryInfo()
            {
                return m_objBECategoryInfo;
            }

            void _IStdBEInfRelacionada.set_BECategoryInfo(ref StdBECategoryInfo value)
            {
                m_objBECategoryInfo = value;
            }               
        #endregion
    }
}

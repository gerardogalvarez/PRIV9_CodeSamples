using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;
using WebServices.WSClasses;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebServices
{
    /// <summary>
    /// Summary description for PRIMAVERA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PRIMAVERA : System.Web.Services.WebService
    {

        public Authentication autorizacao;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string ExisteClienteNIF(string NIF)
        {
            Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();
            BSO.AbreEmpresaTrabalho(0, "DEMO", "primavera", "123");

            string cliente = BSO.Comercial.Clientes.ExisteContribuinte(NIF);
            return cliente;
        }

        [WebMethod]
        public ConsultaNIF ExisteClienteNIF_Ex(string NIF)
        {
            Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();
            BSO.AbreEmpresaTrabalho(0, "DEMOES", "Administrator", "");

            ConsultaNIF resultado = new ConsultaNIF();
            string cliente = BSO.Comercial.Clientes.ExisteContribuinte(NIF);
            if (cliente != "")
            {
                resultado.existe = true;
                resultado.codigo = cliente;
                resultado.nome = BSO.Comercial.Clientes.DaNome(cliente);
            }
            else
            {
                resultado.existe = false;
            }

            return resultado;
        }

        [WebMethod]
        public string AtualizarClientes(Cliente cliente)
        {
            Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();
            BSO.AbreEmpresaTrabalho(0, "DEMO", "primavera", "123");

            Interop.GcpBE900.GcpBECliente objCliente;

            objCliente = BSO.Comercial.Clientes.Edita(cliente.cliente);

            if (objCliente == null)
            {
                objCliente = new Interop.GcpBE900.GcpBECliente();
                objCliente.set_Cliente(cliente.cliente);
                objCliente.set_Moeda(BSO.Contexto.MoedaBase);
            }

            objCliente.set_Nome(cliente.nome);
            objCliente.set_Morada(cliente.morada1);
            objCliente.set_Morada(cliente.morada2);
            objCliente.set_NumContribuinte(cliente.nif);
            objCliente.set_Localidade(cliente.localidade);
            objCliente.set_CodigoPostal(cliente.codPostal);

            BSO.Comercial.Clientes.Actualiza(objCliente);

            return objCliente.get_Cliente();
        }

        [WebMethod]
        [SoapHeader("autorizacao")]
        public List <Cliente> ConsultaClientes() {

            if (!ValidaAutenticacao())
                throw new Exception("Credenciais de acesso inválidas");

            Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();
            //BSO.AbreEmpresaTrabalho(0, "DEMO", "primavera", "123");
            BSO = ERP.AbrirERP();

            string sql = "SELECT cliente,nome, NumContrib FROM Clientes";

            List<Cliente> result = new List<Cliente>();

            Interop.StdBE900.StdBELista objLista;
            objLista = BSO.Consulta(sql);

            while (!objLista.NoFim())
            {
                Cliente objCliente = new Cliente();
                objCliente.cliente = objLista.Valor("Cliente").ToString();
                objCliente.nome = objLista.Valor("Nome").ToString();
                objCliente.nif = objLista.Valor("NumContrib").ToString();
                result.Add(objCliente);

                objLista.Seguinte();
            }

            return result;
        }

        [WebMethod]
        public EncomendaResultado CriarEncomenda(Encomenda enc)
        {
            EncomendaResultado resultado = new EncomendaResultado();
            try
            {
                Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();
                //BSO.AbreEmpresaTrabalho(0, "DEMO", "primavera", "123");
                BSO = ERP.AbrirERP();

                Interop.GcpBE900.GcpBEDocumentoVenda objDocVenda = new Interop.GcpBE900.GcpBEDocumentoVenda();

                objDocVenda.set_Tipodoc(enc.tipodoc);
                objDocVenda.set_Serie(BSO.Comercial.Series.DaSerieDefeito("V", enc.tipodoc));
                objDocVenda.set_TipoEntidade("C");
                objDocVenda.set_Entidade(enc.cliente);

                BSO.Comercial.Vendas.PreencheDadosRelacionados(objDocVenda);

                foreach (LinhaEncomenda linha in enc.linhas)
                {
                    BSO.Comercial.Vendas.AdicionaLinha(objDocVenda, linha.artigo, linha.quantidade, PrecoUnitario: linha.precoUnitario);
                }

                BSO.Comercial.Vendas.Actualiza(objDocVenda);

                string strRefDoc = objDocVenda.get_Tipodoc() + objDocVenda.get_NumDoc() + "/" + objDocVenda.get_Serie();
                resultado.erro = false;
                resultado.referencia = strRefDoc;
            }
            catch (Exception ex)
            {
                resultado.erro = true;
                resultado.descricaoErro = ex.Message;
            }

            return resultado;
        }


        // Não podem existir métodos com Interop na assinatura, caso contrárioo assembly resolve não funciona. 
        // Solução: Criar outras classes para implementar este tipo de métodos.

        //private Interop.ErpBS900.ErpBS AbrirERP()
        //{
        //    string empresa = System.Web.Configuration.WebConfigurationManager.AppSettings["ERPCompany"];
        //    string username = System.Web.Configuration.WebConfigurationManager.AppSettings["ERPUsername"];
        //    string password = System.Web.Configuration.WebConfigurationManager.AppSettings["ERPPassword"];

        //    Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();
        //    BSO.AbreEmpresaTrabalho(0, "DEMO", "primavera", "123");

        //    return BSO;
        //}

        private bool ValidaAutenticacao() {
            //if (autorizacao != null)
            //{
            //  if (autorizacao.User == "PRIMAVERA" && autorizacao.Password == "PRIMAVERA")
                    return true;
            //else
            //    return false;
            //}
            //else
            //    return false;
        }

        public PRIMAVERA()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;
            AssemblyName assemblyName = new AssemblyName(args.Name);

            if (args.Name.StartsWith("Interop") || args.Name.StartsWith("ADODB"))
            {
                string strFolderProgFiles;
                string PRIMAVERA_COMMON_FILES_FOLDER = "PRIMAVERA\\SG900";

                strFolderProgFiles = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);
                assemblyFullName = System.IO.Path.Combine(System.IO.Path.Combine(strFolderProgFiles, PRIMAVERA_COMMON_FILES_FOLDER), assemblyName.Name + ".dll");

                if (System.IO.File.Exists(assemblyFullName))
                {
                    return Assembly.LoadFile(assemblyFullName);
                }
            }

            //File not Found
            return null;
        }


    }
}

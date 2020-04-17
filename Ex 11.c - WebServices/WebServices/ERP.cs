using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices
{
    public class ERP
    {
        public static Interop.ErpBS900.ErpBS AbrirERP()
        {
            string empresa = string.Empty;
            string username = string.Empty;
            string password = string.Empty;

            // ----------------------------------------------------
            // Cargar desde el fichero de configuración Web.config:
            // ----------------------------------------------------
            //empresa = System.Web.Configuration.WebConfigurationManager.AppSettings["ERPCompany"];
            //username = System.Web.Configuration.WebConfigurationManager.AppSettings["ERPUsername"];
            //password = System.Web.Configuration.WebConfigurationManager.AppSettings["ERPPassword"];

            // Datos hardcore:
            empresa = "DEMOES";
            username = "Administrator";
            password = "";

            // Instancia/inicializa los motores PRIMAVERA:
            Interop.ErpBS900.ErpBS BSO = new Interop.ErpBS900.ErpBS();

            // Abre la empresa:
            BSO.AbreEmpresaTrabalho(0, empresa, username, password);

            return BSO;
        }
    }
}
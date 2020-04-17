using System.Data.SqlClient;

namespace ExternalDLL
{
    public class Proxy
    {
        // Variable para guardar los motores (engine) PRIMAVERA:
        public static Interop.ErpBS900.ErpBS BSO;

        // Variable para guardar la Plataforma PRIMAVERA:
        public static Interop.StdPlatBS900.StdBSInterfPub Plataforma;

        // Variable para guardar la connection string:
        public static SqlConnection SQLConnection;

        // Pasamos el contexto a la DLL externa:
        public void setERPContext(Interop.ErpBS900.ErpBS erpBSO, Interop.StdPlatBS900.StdBSInterfPub erpPlat)
        {

            // Los motores (engine):
            BSO = erpBSO;

            // La Plataforma:
            Plataforma = erpPlat;

            // Creamos una conexión a la base de datos a través del ConnectionString:
            SQLConnection = new SqlConnection(Plataforma.BaseDados.DaConnectionStringNET("PRI" + BSO.Contexto.CodEmp, BSO.Contexto.Instancia));
            SQLConnection.Open();
        }

        // Método para mostrar mi formulario:
        public void showForm1()
        {
            Form1 f1 = new Form1();

            f1.ShowDialog();
        }
    }
}

using System.Data.SqlClient;

namespace ExternalDLL
{

    public class Proxy
    {
        // Variable para guardar los motores (engine) PRIMAVERA:
        public static Interop.ErpBS900.ErpBS BSO;

        // Variable para guardar la Plataforma PRIMAVERA:
        public static Interop.StdPlatBS900.StdBSInterfPub Plataforma;

        // Pasamos el contexto a la DLL externa:
        public void setERPContext(Interop.ErpBS900.ErpBS erpBSO, Interop.StdPlatBS900.StdBSInterfPub erpPlat)
        {

            // Los motores (engine):
            BSO = erpBSO;

            // La Plataforma:
            Plataforma = erpPlat;
        }

    }
}

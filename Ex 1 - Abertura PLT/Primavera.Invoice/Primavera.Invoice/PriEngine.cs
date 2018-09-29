using System;
using System.Runtime.InteropServices;
using Interop.ErpBS900;
using Interop.StdBE900;
using Interop.StdPlatBS900;

namespace Primavera.Invoice.Engine
{
    public static class PriEngine
    {

        #region Public Properties
        /// <summary>
        /// Returns the platform
        /// </summary>
        public static StdPlatBS Platform { get; set; }

        /// <summary>
        ///  Returns the engine that allows acess to the modules.
        /// </summary>
        public static ErpBS Engine { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Only intialized in the frist time.
        /// </summary>
        /// <param name="Company"></param>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        public static bool InitializeCompany(string Company, string User, string Password)
        {

            StdBSConfApl objAplConf = new StdBSConfApl();
            StdPlatBS Plataforma = new StdPlatBS();
            ErpBS MotorLE = new ErpBS();

            EnumTipoPlataforma objTipoPlataforma = new EnumTipoPlataforma();
            objTipoPlataforma = EnumTipoPlataforma.tpEmpresarial;

            objAplConf.Instancia = "Default";
            objAplConf.AbvtApl = "ERP";
            objAplConf.PwdUtilizador = Password;
            objAplConf.Utilizador = User;
            objAplConf.LicVersaoMinima = "9.00";

            StdBETransaccao objStdTransac = new StdBETransaccao();

            try
            {
                Plataforma.AbrePlataformaEmpresa(ref Company, ref objStdTransac, ref objAplConf, ref objTipoPlataforma, "");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (Plataforma.Inicializada)
            {

                Platform = Plataforma;

                bool blnModoPrimario = true;

                MotorLE.AbreEmpresaTrabalho(EnumTipoPlataforma.tpEmpresarial, ref Company, ref User, ref Password, ref objStdTransac, "Default", ref blnModoPrimario);
                MotorLE.set_CacheActiva(true);

                Engine = MotorLE;

                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Termina e liberta todos os recursos.
        /// </summary>
        public static void Termina()
        {

            if (Engine != null)
            {
                Engine.FechaEmpresaTrabalho();
                FinalizeConnector(Engine);
            }


            if (Platform != null)
            {
                Platform.FechaPlataformaEx();
                FinalizeConnector(Platform);
            }

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Finalizes the connector.
        /// </summary>
        private static void FinalizeConnector(dynamic ObjCom)
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
                throw new Exception(e.Message);
            }
        }

        #endregion

    }
}

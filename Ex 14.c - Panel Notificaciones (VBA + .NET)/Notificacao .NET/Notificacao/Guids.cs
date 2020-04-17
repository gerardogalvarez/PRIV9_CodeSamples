namespace Notificacao
{
    /// <summary>
    /// Provides the identity for exposed COM classes and interfaces.
    /// If you change them, existing clients will no longer be able to access the class.
    /// Also be aware that COM instantiation uses Namespace for late binding and AssemblyName for direct reference binding.
    /// Therefore changing the AssemblyName or Namespace requires the generation of new IDs to avoid conflicts.
    /// </summary>
    internal static class Guids
    {
        #region Assembly

        /// <summary>
        /// Assembly identity for COM.
        /// </summary>
        public const string Assembly = "B7EFE89A-126E-49DD-804D-5664BA085448";

        #endregion

        #region Erp

        /// <summary>
        /// 
        /// </summary>
        public const string ClassId = "27B2DD60-1B30-4EB6-B288-0B26A47C0C0A";

        /// <summary>
        /// 
        /// </summary>
        public const string InterfaceId = "A8CB700B-8C71-4C7A-AAAE-7B8C62D9A96A";

        /// <summary>
        /// 
        /// </summary>
        public const string EventsId = "EA7618C2-5143-477F-A32E-A34448B46504";

        #endregion

    }

}
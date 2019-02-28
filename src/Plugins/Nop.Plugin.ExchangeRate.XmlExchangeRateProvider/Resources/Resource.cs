using System;

namespace Nop.Plugin.ExchangeRate.XmlExchangeRateProvider.Resources
{
    static class Resource
    {
        static readonly RuntimeTypeHandle currentTypeHandle = typeof(Resource).TypeHandle;
        internal static readonly string ExchangeRateSerialization_xsd = "ExchangeRate-serialization.xsd";
        internal static readonly string NBU_xslt = "NBU.xslt";
        public static System.IO.Stream GetResourceStream(string resourceName)
        {

            return Type
                .GetTypeFromHandle(currentTypeHandle)
                .Assembly.GetManifestResourceStream(resourceName);
        }
    }
}

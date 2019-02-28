
using Nop.Core.Domain.Directory;
using Nop.Core.Plugins;
using Nop.Plugin.ExchangeRate.XmlExchangeRateProvider.Resources;
using Nop.Services.Directory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Nop.Plugin.ExchangeRate.XmlExchangeRateProvider
{
    public class XmlExchangeRateProvider : BasePlugin, IExchangeRateProvider
    {

        public IList<Core.Domain.Directory.ExchangeRate> GetCurrencyLiveRates(string exchangeRateCurrencyCode)
        {
            XDocument xslt_src;
            using (var stream = Resources.Resource.GetResourceStream(Resource.NBU_xslt))
            {
                xslt_src = XDocument.Load(stream);
            }
            var xslt = new XslCompiledTransform();

            xslt.Load(xslt_src.ToXPathNavigable());

            using (var outputStream = new MemoryStream())
            {
                xslt.Transform("", null, outputStream);
            }
            XmlSchema schema_src;
            using (var stream = Resources.Resource.GetResourceStream(Resource.ExchangeRateSerialization_xsd))
            {
                schema_src = XmlSchema.Read(stream, WriteValidation);
            }
            var schemas = new XmlSchemaSet();

            schemas.Add(schema_src);
            var xdoc = XDocument.Load("output.xml");
            xdoc.Validate(schemas, WriteValidation, true);
            return null;

        }
        void WriteValidation(object o, ValidationEventArgs e)
        {
            Console.WriteLine($"{o} -- {e.Message}");
        }
        public override string GetConfigurationPageUrl()
        {
            return base.GetConfigurationPageUrl();
        }
    }
}

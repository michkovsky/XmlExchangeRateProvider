using Nop.Plugin.ExchangeRate.XmlExchangeRateProvider.Resources;
using Nop.Services.Logging;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Nop.Plugin.ExchangeRate.XmlExchangeRateProvider.Services
{
    public class XmlTransformationService : IXmlTransformationService
    {
        private readonly ILogger _logger;
        public XmlTransformationService(ILogger logger) {
            _logger = logger;
        }

        public bool ValidateOutputXml(XDocument xdocExchangeRate)
        {
            var schemas = GetExchangeRateXmlSchemaSet();
            bool ret = true;
            var checkValidation = new ValidationEventHandler((o, e) => {
                ret = false;
                WriteValidation(o, e);
            });
            xdocExchangeRate.Validate(schemas, checkValidation, true);
            return ret;
        }
        XmlSchemaSet GetExchangeRateXmlSchemaSet()
        {
            XmlSchema schema;
            using (var stream = Resources.Resource.GetResourceStream(Resource.ExchangeRateSerialization_xsd))
            {
                schema = XmlSchema.Read(stream, WriteValidation);
            }
            var schemas = new XmlSchemaSet();
            schemas.Add(schema);
            return schemas;
        }
        void WriteValidation(object o, ValidationEventArgs e)
        {
            _logger.Warning($"{typeof(XmlExchangeRateProvider).FullName}: {e.Message} \n {o} ");
        }
    }
}

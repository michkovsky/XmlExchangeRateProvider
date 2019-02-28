using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Nop.Plugin.ExchangeRate.XmlExchangeRateProvider.Services
{
    public interface IXmlTransformationService
    {
        bool ValidateOutputXml(XDocument xdocExchangeRate);
    }
}

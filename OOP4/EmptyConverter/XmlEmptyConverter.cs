using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;
using System.Text.RegularExpressions;

namespace EmptyConverter
{
    public class XmlEmptyConverter: IConverter
    {
        public string Read (string xmlString)
        {
            string result = xmlString;
            result = Regex.Replace(result, "XmlEmptyConverter\r\n", String.Empty);
            return xmlString;            
        }

        public string Write (string xmlString)
        {
            return "XmlEmptyConverter\r\n" + xmlString;
        }
    }
}

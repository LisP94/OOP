using Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AttributesConverter
{
    public class XmlChangeAttributes: IConverter
    {
        public string Write(string xmlString)
        {
            string result = xmlString;
            MatchCollection matches = Regex.Matches(result, "<Product(.*?)>");
            foreach (Match match in matches)
            {
                string currentMatch = match.Value;
                string attribut = String.Empty;
                string value = String.Empty;
                string temp = String.Empty;
                for (int i=0; i<currentMatch.Length; i++)
                {
                    if (currentMatch[i] == '=')
                    {
                        attribut = temp;
                        temp = String.Empty;
                    }
                    if (currentMatch[i] == '"')
                    {
                        value = temp;
                        temp = String.Empty;
                    }
                    if (currentMatch[i] != '"')
                    {
                        temp += currentMatch[i];
                    }
                    if (currentMatch[i] == ' ')
                    {
                        temp = String.Empty;
                    }
                }
                temp = "<Product>\r\n  <" + attribut + ">" + value + "</" + attribut + ">";
                result = Regex.Replace(result, currentMatch, temp);
            }
            return "XmlChangeAttributes\r\n" + result;
        }

        public string Read(string xmlString)
        {
            string result = xmlString;
            result = Regex.Replace(result, "XmlChangeAttributes\r\n", String.Empty);
            MatchCollection matches = Regex.Matches(result, "<Product>\r\n(.*?)\r\n");
            foreach (Match match in matches)
            {
                string currentMatch = Regex.Match(match.Value, "\r\n(.*?)</").Value;
                string attribut = String.Empty;
                string value = String.Empty;
                string temp = String.Empty;
                for (int i=0; i<currentMatch.Length; i++)
                {
                    if (currentMatch[i] == '<')
                    {
                        value = temp;
                        temp = String.Empty;
                    }
                    if (currentMatch[i] == '>')
                    {
                        attribut = temp;
                        temp = String.Empty;
                    }
                    if (currentMatch[i] != '<' && currentMatch[i] != '>')
                    {
                        temp += currentMatch[i];
                    }
                }
                temp = "<Product " + attribut + "=\"" + value + "\">\r\n";
                result = Regex.Replace(result, match.Value, temp);
            }
            return result;
        }
    }
}

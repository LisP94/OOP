using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public interface IConverter
    {
        string Read(string xmlString);
        string Write(string xmlString);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products
{
    [Serializable]
    public abstract class Product
    {
        public string Taste { get; set; }
        public int Weight { get; set; }
    }
}

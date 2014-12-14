using Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruits
{
    [Serializable]
    public abstract class Fruit: Product
    {
        public string Color { get; set; }
    }
}

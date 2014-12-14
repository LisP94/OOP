using Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatL
{
    [Serializable]
    public abstract class Meat: Product
    {
        public int Fatness { get; set; }
        public int Softness { get; set; }
    }
}

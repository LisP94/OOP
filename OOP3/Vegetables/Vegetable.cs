using Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegetables
{
    [Serializable]
    public abstract class Vegetable: Product
    {
        public int Size { get; set; }
    }
}

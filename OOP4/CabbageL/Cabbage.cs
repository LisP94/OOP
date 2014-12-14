using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetables;

namespace CabbageL
{
    [Serializable]
    public class Cabbage : Vegetable
    {
        public Cabbage()
        {
            var random = new Random();
            LeafCount = random.Next(10, 30);
            Size = random.Next(10, 100);
            Taste = "Cabbage";
            Weight = random.Next(10, 100);
        }
        public int LeafCount { get; set; }
    }
}

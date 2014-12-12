using MeatL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefL
{
    [Serializable]
    public class Beef: Meat
    {
        public Beef()
        {
            var random = new Random();
            Fatness = random.Next(0, 100);
            Taste = "Meat";
            Roasted = 0;
            Softness = random.Next(0, 100);
            Weight = random.Next(0, 1000);
        }
        public int Roasted { get; set; }
    }
}

using MeatL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorkL
{
    [Serializable]
    public class Pork: Meat
    {
        public Pork()
        {
            var random = new Random();
            Fatness = random.Next(0, 100);
            Taste = "Meat";
            Jerking = 0;
            Softness = random.Next(0, 100);
            Weight = random.Next(0, 1000);
        }
        public int Jerking { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetables;

namespace PotatoL
{
    [Serializable]
    public class Potato: Vegetable
    {
        public Potato()
        {
            var random = new Random();
            Size = random.Next(10, 100);
            StarchCount = random.Next(10, 100);
            Taste = "Potato";
            Weight = random.Next(10, 100);
        }
        public int StarchCount { get; set; }
    }
}

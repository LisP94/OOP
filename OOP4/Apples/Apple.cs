using Fruits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apples
{
    [Serializable]
    public class Apple: Fruit
    {
        public Apple()
        {
            var random = new Random();
            Color = "Red";
            Swetness = random.Next(10, 100);
            Taste = "Sour";
            Weight = random.Next(10, 100);
        }
        public int Swetness { get; set; }
    }
}

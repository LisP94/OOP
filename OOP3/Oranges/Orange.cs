using Fruits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oranges
{
    [Serializable]
    public class Orange: Fruit
    {
        public Orange()
        {
            var random = new Random();
            Color = "Orange";
            Juiciness = random.Next(10, 100);
            int countOfSlice = random.Next(8, 12);
            Slice = new OrangeSlice[countOfSlice];
            for (int i = 0; i < countOfSlice; i++)
            {
                Slice[i] = new OrangeSlice();
                Slice[i].GrainCount = random.Next(100, 500);
                Slice[i].SeedCount = random.Next(0, 4);
            }
            Taste = "Sweet";
            Weight = random.Next(10, 100);
        }
        public int Juiciness { get; set; }
        public OrangeSlice[] Slice { get; set; }
    }

    [Serializable]
    public class OrangeSlice
    {
        public int GrainCount { get; set; }
        public int SeedCount { get; set; }
    }
}

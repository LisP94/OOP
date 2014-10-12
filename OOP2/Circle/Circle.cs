using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circle
{
    public class Circle : Shapes.Shapes
    {
        Point centre;
        int radius;
        public Circle(Point[] point)
        {
            centre = point[0];
            radius =(int) Math.Sqrt((point[0].X - point[1].X)*(point[0].X - point[1].X)+(point[0].Y - point[1].Y)*
                     (point[0].Y - point[1].Y));
        }
        public override void draw(Graphics e)
        {
            e.DrawEllipse(Pens.Blue, centre.X - radius, centre.Y - radius, 2 * radius, 2 * radius);
        }
    }
}


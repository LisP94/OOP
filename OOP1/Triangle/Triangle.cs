using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Triangle
{
    public class Triangle: Polygon.Polygon
    {
        public Triangle(Point[] contour):base (contour)
        {
        }
        public override void draw(Graphics e)
        {
            e.DrawLine(Pens.Red, curve[0].X, curve[0].Y, curve[1].X, curve[1].Y);
            e.DrawLine(Pens.Red, curve[1].X, curve[1].Y, curve[2].X, curve[2].Y);
            e.DrawLine(Pens.Red, curve[2].X, curve[2].Y, curve[0].X, curve[0].Y);
        }
    }
}

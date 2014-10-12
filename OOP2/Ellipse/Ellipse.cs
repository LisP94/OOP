using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ellipse
{
    public class Ellipse: Shapes.Shapes
    {
        Point begin;
        Size size;
        public Ellipse(Point[] point)
        {
            begin=point[0];
            size.Width = point[1].X - point[0].X;
            size.Height = point[1].Y-point[0].Y;
        }
        public override void draw(Graphics e)
        {
            e.DrawEllipse(Pens.Green, begin.X,begin.Y, size.Width,size.Height);
        }
    }
}

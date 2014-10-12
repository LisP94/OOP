using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Rectangle
{
    public class Rectangle: Polygon.Polygon
    {
        Point begin;
        Size size;
        public Rectangle(Point[] contour) : base (contour)
        {
            begin.X = contour[0].X;
            begin.Y = contour[0].Y;
            size.Width = contour[1].X-contour[0].X;
            size.Height = contour[1].Y-contour[0].Y;
        }
        public override void draw(Graphics e)
        {
            e.DrawRectangle(Pens.Red,begin.X,begin.Y,size.Width,size.Height);
        }
    }
}

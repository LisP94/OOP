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
            if (contour[0].X>contour[1].X)
            {
                begin.X = contour[1].X;
                size.Width = contour[0].X - contour[1].X;
            }
            else
            {
                begin.X = contour[0].X;
                size.Width = contour[1].X - contour[0].X;
            }
            if (contour[0].Y>contour[1].Y)
            {
                begin.Y = contour[1].Y;
                size.Height = contour[0].Y - contour[1].Y;
            }
            else
            {
                begin.Y = contour[0].Y;
                size.Height = contour[1].Y - contour[0].Y;
            }
        }
        public override void draw(Graphics e)
        {
            e.DrawRectangle(Pens.Red,begin.X,begin.Y,size.Width,size.Height);
        }
    }
}

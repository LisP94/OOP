using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Line
{
    public class Line: Shapes.Shapes
    {
        Point begin,end;
        public Line(Point[] points)
        {
            begin = points[0];
            end = points[1];
        }
        public override void draw(Graphics e)
        {
            e.DrawLine(Pens.Red, begin.X, begin.Y, end.X, end.Y);
        }
    }
}

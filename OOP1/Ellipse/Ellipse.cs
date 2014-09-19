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
        Point point;
        Size ellsize;
        public Ellipse(Point centre, int a, int b)
        {
            point = centre;
            ellsize.Width = a * 2;
            ellsize.Height = b * 2;
        }
        public override void draw(Graphics e)
        {
            e.DrawEllipse(Pens.Red, point.X,point.Y, ellsize.Width,ellsize.Height);
        }
    }
}

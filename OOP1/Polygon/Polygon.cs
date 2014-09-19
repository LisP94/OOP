using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Polygon
{
    public class Polygon: Shapes.Shapes
    {
        protected Point[] curve;
        public Polygon(Point[] contour)
        {
            curve = contour;
        }
        public override void draw(Graphics e)
        {
            e.DrawPolygon(Pens.Red, curve);
        }
    }
}

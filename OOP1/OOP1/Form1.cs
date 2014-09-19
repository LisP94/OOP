using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP1
{
    public partial class Form1 : Form
    {
        Graphics g;
        int i;
        Random rand=new Random();
        Point point = new Point();
        Point [] points;
        bool polygon;
        Shapes.Shapes shape;
        List<Shapes.Shapes> list;
        List<Point> cont;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            list= new List<Shapes.Shapes>();
            cont = new List<Point>();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            points = new Point[2];
            for (i = 0; i <= 1; i++)
            {
                points[i].X = rand.Next(0, 300);
                points[i].Y = rand.Next(0, 300);
            }
            shape = new Line.Line(points);
            shape.draw(g);
            list.Add(shape);
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int a,b;
            point.X = rand.Next(120, 170);
            point.Y = rand.Next(0, 300);
            a = rand.Next(2, 80);
            b = rand.Next(2, 80);
            shape = new Ellipse.Ellipse(point, a,b);
            shape.draw(g);
            list.Add(shape);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            points = new Point[2];
            points[0].X = rand.Next(0, 150);
            points[0].Y = rand.Next(0, 150);
            points[1].X = rand.Next(150, 300);
            points[1].Y = rand.Next(150, 300);
            shape = new Rectangle.Rectangle(points);
            shape.draw(g);
            list.Add(shape);
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            points = new Point[3];
            points[0].X = rand.Next(0, 300);
            points[0].Y = rand.Next(0, 300);
            points[1].X = rand.Next(0, 300);
            points[1].Y = rand.Next(0, 300);
            points[2].X = rand.Next(0, 300);
            points[2].Y = rand.Next(0, 300);
            shape = new Triangle.Triangle(points);
            shape.draw(g);
            list.Add(shape);
        }

        private void printListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i;
            for (i=0; i<list.Count;i++)
            {
                list.ElementAt(i).draw(g);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygon = true;
            i = 0;
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int count;
            if (polygon)
            {
                if (i > 0)
                {
                    if ((Math.Abs(e.X - cont.ElementAt(0).X) < 5) && (Math.Abs(e.Y - cont.ElementAt(0).Y) < 5))
                    {
                        g.DrawLine(Pens.Red, cont.ElementAt(i - 1), cont.ElementAt(0));
                        polygon = false;
                        points = new Point[cont.Count];
                        for (count = 0; count < cont.Count; count++)
                        {
                            points[count] = cont.ElementAt(count);
                        }
                        shape = new Polygon.Polygon(points);
                        shape.draw(g);
                        list.Add(shape);
                    }
                    else
                    {
                        point.X = e.X;
                        point.Y = e.Y;
                        g.DrawLine(Pens.Red, point, cont.ElementAt(i - 1));
                        cont.Add(point);
                    }
                }
                else
                {
                    point.X = e.X;
                    point.Y = e.Y;
                    cont.Add(point);
                }
                i++;
            }
        }
    }
}

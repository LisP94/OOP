using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace OOP2
{
    public partial class Form1 : Form
    {
        Dictionary<string, Type> typesname = new Dictionary<string, Type>();
        List<Point> mousepoz = new List<Point>();
        Type type;
        bool choice = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(folder, "*.dll");
            for (int i = 0; i < files.Length; i++)
            {
                Assembly assembly = Assembly.LoadFile(files[i]);
                Type[] type = assembly.GetTypes();
                for (int j = 0; j < type.Length;j++ )
                {
                    typesname.Add(type[j].Name, type[j]);
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = type[j].Name;
                    item.Click += new EventHandler(MenuClick);
                    toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { item });
                }
            }
        }
        private void MenuClick(object sender, EventArgs e)
        {
            type = typesname[sender.ToString()];
            choice = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new Point();
            point.X = e.X;
            point.Y = e.Y;
            mousepoz.Add(point);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && choice)
            {
                if (mousepoz.Count < 2)
                {
                    MessageBox.Show("Enter >2 point!","ERROR");
                    return;
                }
                Type[] args = new Type[1];
                args[0] = typeof(Point[]);
                object[] par = new object[1];
                List<Point> list = new List<Point>();
                list = mousepoz.GetRange(0, mousepoz.Count);
                mousepoz.Clear();
                par[0] = list.ToArray();
                ConstructorInfo ci = type.GetConstructor(args);
                FieldInfo[] fi = type.GetFields();
                if (ci != null)
                {
                    object shape = ci.Invoke(par);
                    MethodInfo[] mi = type.GetMethods();
                    if (mi != null)
                    {
                        object[] metharg = new object[1];
                        Graphics g = this.CreateGraphics();
                        metharg[0] = g;
                        mi[0].Invoke(shape, metharg);
                    }
                }

            }
        }

    }
}

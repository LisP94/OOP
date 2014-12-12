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
using Products;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace OOP3
{
    public partial class Form1 : Form
    {
        Dictionary<string, Type> typesname;
        List<Product> productList;
        TreeNode choseNode;
        TreeNodeTag selectedNode;
        public Form1()
        {
            InitializeComponent();
            typesname = new Dictionary<string, Type>();
            productList = new List<Product>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(folder, "*.dll");
            for (int i = 0; i < files.Length; i++)
            {
                Assembly assembly = Assembly.LoadFile(files[i]);
                Type[] type = assembly.GetTypes();
                for (int j = 0; j < type.Length; j++)
                {
                    typesname.Add(type[j].Name, type[j]);
                    if (type[j].IsAbstract == false && type[j].IsSubclassOf(typeof(Product)))
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Text = type[j].Name;
                        item.Click += new EventHandler(MenuClick);
                        productsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { item });
                    }
                }
            }
        }

        private void MenuClick(object sender, EventArgs e)
        {
            Type type = typesname[sender.ToString()];
            ConstructorInfo ci = type.GetConstructor(new Type[] { });
            if (ci != null)
            {
                Product product = (Product)ci.Invoke(new object[] { });
                productList.Add(product);
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(TreeBuilder.GetTree(productList));
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeTag nodeTag = e.Node.Tag as TreeNodeTag;
            selectedNode = nodeTag;
            if (nodeTag != null)
            {
                if (nodeTag.NodeType.IsValueType || nodeTag.Value is String)
                {
                    choseNode = e.Node;
                    textBox1.Text = nodeTag.Value.ToString();
                    textBox1.Visible = true;
                    button1.Visible = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (choseNode != null)
            {
                TreeNodeTag nodeTag = (TreeNodeTag)choseNode.Tag;
                TreeNodeTag parentTag = (TreeNodeTag)choseNode.Parent.Tag;
                choseNode.Name = nodeTag.PropertiesInfo.Name + " = ";
                if (nodeTag.PropertiesInfo.CanWrite)
                {
                    if (nodeTag.Value is int)
                    {
                        int value;
                        if (int.TryParse(textBox1.Text, out value))
                        {
                            choseNode.Name += textBox1.Text;
                            nodeTag.PropertiesInfo.SetValue(parentTag.Value, value);
                            nodeTag.Value = value;
                        }
                        else
                        {
                            MessageBox.Show("Incorrect input.");
                        }
                    }
                    else
                    {
                        if (nodeTag.Value is String)
                        {
                            choseNode.Name += textBox1.Text;
                            nodeTag.PropertiesInfo.SetValue(parentTag.Value, textBox1.Text);
                            nodeTag.Value = textBox1.Text;
                        }
                        else
                        {
                            if (nodeTag.Value is double)
                            {
                                double value;
                                if (double.TryParse(textBox1.Text, out value))
                                {
                                    choseNode.Name += textBox1.Text;
                                    nodeTag.PropertiesInfo.SetValue(parentTag.Value, value);
                                    nodeTag.Value = value;
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect input");
                                }
                            }
                            else
                            {
                                if (nodeTag.Value is bool)
                                {
                                    bool value;
                                    if (bool.TryParse(textBox1.Text, out value))
                                    {
                                        choseNode.Name += textBox1.Text;
                                        nodeTag.PropertiesInfo.SetValue(parentTag.Value, value);
                                        nodeTag.Value = value;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect input");
                                }
                            }
                        }
                    }
                }
            }
            textBox1.Visible = false;
            button1.Visible = false;
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(TreeBuilder.GetTree(productList));
        }

        private void treeView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (selectedNode != null && selectedNode.NodeType.IsSubclassOf(typeof(Product)))
                {
                    if (MessageBox.Show("You really want to delete this object?", "Delete object", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        productList.Remove((Product)selectedNode.Value);
                        treeView1.Nodes.Clear();
                        treeView1.Nodes.Add(TreeBuilder.GetTree(productList));
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = new FileStream("BinSerialize.txt", FileMode.Create))
            {
                binaryFormatter.Serialize(fileStream, productList);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = new FileStream("BinSerialize.txt", FileMode.Open))
            {
                productList.Clear();
                productList = (List<Product>)binaryFormatter.Deserialize(fileStream);
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(TreeBuilder.GetTree(productList));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Type> typeList = new List<Type>();
            foreach (var item in typesname)
            {
                typeList.Add(item.Value);
            }
            Type[] typeArray = typeList.ToArray();
            var xmlSerializer = new XmlSerializer(typeof(List<Product>), typeArray);
            using (var fileStream = new FileStream("XmlSerialize.txt", FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, productList);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Type> typeList = new List<Type>();
            foreach (var item in typesname)
            {
                typeList.Add(item.Value);
            }
            Type[] typeArray = typeList.ToArray();
            var xmlSerializer = new XmlSerializer(typeof(List<Product>), typeArray);
            using (var fileStream = new FileStream("XmlSerialize.txt", FileMode.Open))
            {
                productList.Clear();
                productList = (List<Product>)xmlSerializer.Deserialize(fileStream);
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(TreeBuilder.GetTree(productList));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var textSerializer = new Serializer(typesname);
            using (var fileStream = new FileStream("TextSerialize.Txt", FileMode.Create))
            {
                var stream = new StreamWriter(fileStream) { AutoFlush = true };
                textSerializer.Serialize(stream, productList);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var textSerializer = new Serializer(typesname);
            using (var fileStream = new FileStream("TextSerialize.Txt", FileMode.Open))
            {
                var stream = new StreamReader(fileStream);
                productList = (List<Product>)textSerializer.Deserialize(stream);
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(TreeBuilder.GetTree(productList));
            }
        }
    }
}

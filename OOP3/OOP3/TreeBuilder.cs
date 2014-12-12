using Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace OOP3
{
    static class TreeBuilder
    {
        public static TreeNode GetTree(List<Product> items)
        {
            TreeNode result = new TreeNode("Products")
            {
                Tag = items
            };
            foreach (var item in items)
            {
                result.Nodes.Add(GetNodeByItem(item));
            }

            return result;
        }

        private static TreeNode GetNodeByItem(object item)
        {
            Type itemType = item.GetType();
            TreeNode result = new TreeNode(itemType.Name)
            {
                Tag = new TreeNodeTag { NodeType = itemType, Value = item }
            };
            PropertyInfo[] properties = itemType.GetProperties();
            foreach (var property in properties)
            {
                result.Nodes.Add(GetNodeByProperty(property.GetValue(item), property));
            }

            return result;
        }

        private static TreeNode GetNodeByProperty(object item, PropertyInfo itemProperty)
        {
            Type itemType = item.GetType();
            TreeNode result = null;            
            if (!itemType.IsValueType && !(item is String))
            {
                if (itemType.IsArray)
                {
                    result = new TreeNode(itemType.Name);
                    Array arrayItem = (Array)item;
                    int index = 0;
                    foreach (var itemFromIndex in arrayItem)
                    {
                        index++;
                        result.Nodes.Add(TreeNodeFromProperty(itemFromIndex, itemProperty, itemFromIndex.GetType(), index.ToString()));
                    }
                }
                else
                {
                    result = TreeNodeFromProperty(item, itemProperty, itemType, null);
                }
            }
            else
            {
                result = new TreeNode(itemProperty.Name + " = " + item);
            }
            result.Tag = new TreeNodeTag
            {
                NodeType = itemType,
                Value = item,
                PropertiesInfo = itemProperty
            };

            return result;
        }

        public static TreeNode TreeNodeFromProperty(object item, PropertyInfo itemProperty, Type itemType, string index)
        {
            TreeNode result = new TreeNode(itemProperty.Name + index);
            PropertyInfo[] properties = itemType.GetProperties();
            foreach (var property in properties)
            {
                result.Nodes.Add(GetNodeByProperty(property.GetValue(item), property));
            }

            return result;
        }


    }
}

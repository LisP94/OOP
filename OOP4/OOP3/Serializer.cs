using Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace OOP3
{
    public class Serializer
    {
        Dictionary<string, Type> allTypes;

        public Serializer(Dictionary<string, Type> types)
        {
            allTypes = types;
        }

        public void Serialize (StreamWriter stream, object serializeObject)
        {
            Type serializeType = serializeObject.GetType();
            if (serializeType.IsGenericType)
            {
                stream.WriteLine(serializeType.Name);
                stream.WriteLine("Count=" + ((IList)serializeObject).Count);
                Type[] itemType = serializeType.GenericTypeArguments;
                if (itemType[0] != null)
                {
                    if (itemType[0].IsClass)
                    {
                        foreach (var item in (IList)serializeObject)
                            SerializeClass(stream, item);
                    }
                }
            }
            else
            {
                if (serializeType.IsClass)
                {
                    SerializeClass(stream, serializeObject);
                }
            }
        }

        void SerializeClass(StreamWriter stream, object serializeObject)
        {
            Type itemType = serializeObject.GetType();
            PropertyInfo[] pi = itemType.GetProperties();
            stream.WriteLine(itemType.Name);
            foreach (var property in pi)
            {
                object value = property.GetValue(serializeObject);
                Type valueType = value.GetType();
                if (valueType.IsPrimitive || value is String)
                {
                    stream.Write(property.Name + "=");
                }
                else
                {
                    if (valueType.IsArray)
                    {
                        stream.WriteLine(property.Name); // braces
                        stream.WriteLine("Length=" + ((Array)value).Length);
                    }
                    else
                    {
                        stream.WriteLine(property.Name);
                    }
                }
                DefineType(stream, property.GetValue(serializeObject));
            }
            stream.WriteLine(itemType.Name);
        }

        void DefineType(StreamWriter stream, object serializeObject)
        {
            Type itemType = serializeObject.GetType();
            if (itemType.IsPrimitive || serializeObject is String)
            {
                SerializePrimitive(stream, serializeObject);
            }
            else
            {
                if (itemType.IsArray)
                {
                    foreach (var item in (Array)serializeObject)
                    {
                        Serialize(stream, item);
                    }
                }               
                else
                {
                    if (itemType.IsClass)
                    {
                        SerializeClass(stream, serializeObject);
                    }
                }
            }
        }

        void SerializePrimitive(StreamWriter stream, object serializeObject)
        {
            if (serializeObject is int)
            {
                stream.WriteLine((int)serializeObject);
            }
            else
            {
                if (serializeObject is double)
                {
                    stream.WriteLine((double)serializeObject);
                }
                else
                {
                    if (serializeObject is bool)
                    {
                        if ((bool)serializeObject)
                        {
                            stream.WriteLine("true");
                        }
                        else
                        {
                            stream.WriteLine("false");
                        }
                    }
                    else
                    {
                        stream.WriteLine((string)serializeObject);
                    }
                }
            }
        }

        public object Deserialize(StreamReader stream)
        {
            List<Product> result = new List<Product>();
            string str = stream.ReadLine();
            if (str.Contains("List"))
            {
                str = stream.ReadLine();
                string tempstr = String.Empty;
                if (str.Contains("Count"))
                {                    
                    for (int i=0; i<str.Length; i++)
                    {
                        if (str[i] == '=')
                        {
                            i++;
                            tempstr = String.Empty;
                        }
                        tempstr += str[i];
                    }
                }
                int count = int.Parse(tempstr);
                for (int i=0; i<count; i++)
                {
                    tempstr = stream.ReadLine();
                    Type itemType = allTypes[tempstr];
                    ConstructorInfo ci = itemType.GetConstructor(new Type[] { });
                    if (ci != null)
                    {
                        object item = ci.Invoke(new object[] { });
                        item = DeserializeProperties(stream, item);
                        result.Add((Product)item);
                    }
                }
            }
            return result;
        }

        object DeserializeProperties(StreamReader stream, object item)
        {
            Type itemType = item.GetType();
            PropertyInfo[] pi = itemType.GetProperties();
            int count = pi.Length;
            string tempstring = stream.ReadLine();
            while (tempstring != itemType.Name)
            {
                string[] valueProperty;
                if (tempstring.Contains("="))
                {
                    valueProperty = DivString(tempstring);
                }
                else
                {
                    valueProperty = new string[2];
                    valueProperty[0] = tempstring;
                }
                for (int i = 0; i < count; i++)
                {
                    if (pi[i].Name == valueProperty[0])
                    {
                        item = DefineType(stream, item, pi[i], valueProperty[1]);
                    }
                }
                tempstring = stream.ReadLine();
            }
            return item;
        }

        object DefineType(StreamReader stream, object item, PropertyInfo pi, string propertyValue)
        {
            Type propertyType = pi.PropertyType;
            if (propertyType.IsPrimitive || propertyType == typeof(String))
            {
                object value = pi.GetValue(item);
                if (value is int)
                {
                    pi.SetValue(item, int.Parse(propertyValue));
                }
                else
                {
                    if (value is double)
                    {
                        pi.SetValue(item, double.Parse(propertyValue));
                    }
                    else
                    {
                        if (value == null || value is String)
                        {
                            pi.SetValue(item, propertyValue);
                        }
                        else
                        {
                            if (value is bool)
                            {
                                pi.SetValue(item, value == "true");
                            }
                        }
                    }
                }
            }
            else
            {
                if (propertyType.IsArray)
                {
                    string tempstr = stream.ReadLine();
                    string[] param = null;
                    param = DivString(tempstr);
                    int length = int.Parse(param[1]);
                    Type[] par = new Type[1];
                    par[0] = typeof(int);
                    ConstructorInfo ci = propertyType.GetConstructor(par);
                    object[] pars = new object[1];
                    pars[0] = length;
                    object[] propertyArray = (object[])ci.Invoke(pars);
                    for (int i = 0; i < length; i++)
                    {
                        tempstr = stream.ReadLine();
                        propertyArray[i] = DeserializeClass(stream, tempstr);
                    }
                    pi.SetValue(item, propertyArray);

                }
                else
                {
                    if (propertyType.IsClass)
                    {
                        pi.SetValue(item, DeserializeClass(stream, pi.Name));
                    }
                }
            }
            return item;
        }

        object DeserializeClass(StreamReader stream, string propertyValue)
        {
            Type itemType = allTypes[propertyValue];
            ConstructorInfo ci = itemType.GetConstructor(new Type[] { });
            if (ci != null)
            {
                object item = ci.Invoke(new object[] { });
                item = DeserializeProperties(stream, item);
                return item;
            }
            return null;
        }

        string[] DivString(string line)
        {
            string[] result = new string[2];
            int i = 0;
            while (line[i] != '=')
            {
                result[0] += line[i];
                i++;
            }
            i++;
            while (i<line.Length)
            {
                result[1] += line[i];
                i++;
            }
            return result;
        }
    }
}

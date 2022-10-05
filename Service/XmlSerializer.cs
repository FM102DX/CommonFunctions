using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pimark.MpeTestingSuite.Service
{
    public class XmlSerializer<T>
    {
        string _filename;

        XmlSerializer _xmlSerializer;

        public static XmlSerializer<T> GetInstance(string filename)
        {
            return new XmlSerializer<T>(filename);
        }

        private XmlSerializer(string filename)
        {
            _filename = filename;
            _xmlSerializer = new XmlSerializer(typeof(T));
        }
        public void Serialize(T t)
        {
            StringWriter textWriter = new StringWriter();
            _xmlSerializer.Serialize(textWriter, t);
            File.WriteAllText(_filename, textWriter.ToString());
        }
        public T Deserialize()
        {
            T t;
            string s = File.ReadAllText(_filename);
            StringReader sr = new StringReader(s);
            t = (T)_xmlSerializer.Deserialize(sr);
            return t;
        }

    }
}

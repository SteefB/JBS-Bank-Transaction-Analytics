using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace BankTransactions.Controllers.Util
{
    public class XMLSerializationService
    {
        public static void Write<T>(T obj, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using(Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stream))
                {
                    serializer.Serialize(xmlWriter, obj);
                }
            }
        }

        public static T Read<T>(string fileName)
        {
            T ret = Activator.CreateInstance<T>();

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (Stream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                try
                {
                    ret = (T)serializer.Deserialize(stream);
                }
                catch (InvalidOperationException ex)
                {
                    // Log
                }
            }

            return ret;
        }
    }
}
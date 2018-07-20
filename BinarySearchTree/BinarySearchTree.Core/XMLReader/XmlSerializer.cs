using System;
using System.IO;
using System.Xml.Serialization;

namespace BinarySearchTree.Core.XMLReader
{
    public class XmlSerializer<T>
    {
        /// <summary>
        /// Deserialize xml from a given xml file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <returns>The object</returns>
        public T Deserialize(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StreamReader(path))
                {
                    return (T) serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
    }
}

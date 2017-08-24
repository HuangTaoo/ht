using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BWP.WinFormBase
{
  public class XmlUtil
  {

    public static readonly string ClientGoodsList = "ClientGoodsList.xml";
    public static void SerializerObjToFile(object obj, string fileName="")
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        fileName = obj.GetType().Name + ".xml";
      }
      var ser = new XmlSerializer(obj.GetType());
      var stream = new FileStream(fileName, FileMode.Create);
      ser.Serialize(stream, obj);
      stream.Close();
    }


    public static T DeserializeFromFile<T>(string fileName="")
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        fileName = typeof(T).Name + ".xml";
      }
      if (!File.Exists(fileName))
      {
        throw new Exception("不存在文件：" + fileName);
      }
      using (var reader = new StreamReader(fileName))
      {
        var xs = new XmlSerializer(typeof(T));
        object obj = xs.Deserialize(reader);
        reader.Close();
        return (T)obj;
      }
    }


    public static string ObjectSerializeXml<T>(T obj)
    {
      using (MemoryStream ms = new MemoryStream())
      {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(ms, obj);
        ms.Seek(0, SeekOrigin.Begin);
        using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
        {
          return reader.ReadToEnd();
        }
      }
    }


    public static T XmlDeserializeObject<T>(string xmlOfObject) where T : class
    {
      using (MemoryStream ms = new MemoryStream())
      {
        using (StreamWriter sr = new StreamWriter(ms, Encoding.UTF8))
        {
          sr.Write(xmlOfObject);
          sr.Flush();
          ms.Seek(0, SeekOrigin.Begin);
          XmlSerializer serializer = new XmlSerializer(typeof(T));
          return serializer.Deserialize(ms) as T;
        }
      }
    }
  }
}

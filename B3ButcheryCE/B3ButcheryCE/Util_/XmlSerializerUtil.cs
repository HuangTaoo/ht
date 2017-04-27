using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using B3HRCE;
using B3HRCE.Rpc_;

namespace B3ButcheryCE.Util_
{
    public class XmlSerializerUtil
    {
        public static void ClientXmlSerializer(object obj)
        {
            var type = obj.GetType();
            var folder = Path.Combine(Util.DataFolder, type.Name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);

            }
            XmlSerializer serializer = new XmlSerializer(type);

            using (var stream = File.Open(Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xml"), FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }



        public static List<T> GetClientListXmlDeserialize<T>()
        {
            var list = new List<T>();
            var folder = Path.Combine(Util.DataFolder, typeof(T).Name);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);

            }

            var files = Directory.GetFiles(folder, "*.xml");
            foreach (var file in files)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (var stream = File.Open(file, FileMode.Open))
                {
                    var tempList = serializer.Deserialize(stream) as List<T>;
                    list.AddRange(tempList);
                }
            }
            return list;
        }

        ///// <summary>
        ///// 获取已缓存的所有存货信息
        ///// </summary>
        ///// <returns></returns>
        //public static List<ClientGoods> GetLocationClientGoods()
        //{
        //    var list = new List<ClientGoods>();
        //    var folder = Path.Combine(Util.DataFolder, typeof(ClientGoods).Name);

        //    var files = Directory.GetFiles(folder, "*.xml");
        //    foreach (var file in files)
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(List<ClientGoods>));
        //        using (var stream = File.Open(file, FileMode.Open))
        //        {
        //            var tempList = serializer.Deserialize(stream) as List<ClientGoods>;
        //            list.AddRange(tempList);
        //        }
        //    }
        //    return list;

        //}

    }
}

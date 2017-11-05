using Forks.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.Attributes
{
    public class MoneyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {


            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                Money<decimal>? money = value as Money<decimal>?;



                if (money != null)
                {
                    //if (animal is Dog)
                    //{
                    //    o.AddFirst(new JProperty("type", "Dog"));
                    //    //o.Find
                    //}
                    //else if (animal is Cat)
                    //{
                    //    o.AddFirst(new JProperty("type", "Cat"));
                    //}

                    //foreach (IAnimal childAnimal in animal.Children)
                    //{
                    //    // ???
                    //}

                    decimal o = (decimal)money.Value;

                    writer.WriteValue(o);
                    //o.WriteTo(writer);
                }
                
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("不支持反序列化");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DimensionAndSort;
using Newtonsoft.Json;

namespace UnitTests
{
    public class SerializationTests
    {
        [Fact]
        public void SerializationTest01()
        {
            Length l = new Length(10);

            Length l2 = SerializeUnserializeObject(l);

            Assert.Equal(l,l2);

        }

        private T SerializeUnserializeObject<T>(T obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            TextWriter tw = new StringWriter();
            serializer.Serialize(tw, obj);

            string jsonStr = tw.ToString();

            File.WriteAllText("unittest.json", jsonStr);

            T recreated = JsonConvert.DeserializeObject<T>(jsonStr, settings);

            tw = new StringWriter();
            serializer.Serialize(tw, recreated);
            string jsonStr2 = tw.ToString();

            File.WriteAllText("unittest2.json", jsonStr2);

            return recreated;

        }

    }
}

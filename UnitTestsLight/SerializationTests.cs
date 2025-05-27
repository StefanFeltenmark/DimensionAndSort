using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using GreenOptimizer.DimensionAndSort;
using Newtonsoft.Json;

namespace UnitTests
{
    public class SerializationTests
    {
        [Fact]
        public void SerializationTest01()
        {
            Length l = new Length(10);

            DoTestSerialization(l);
        }

        
        public static void DoTestSerialization<T>(T obj)
        {
            Assert.Equal(obj, SerializeUnserializeObject(obj));
        }

        [Fact]
        public void SerializationTest02()
        {
            Length l = new Length(10, Unit.SI_PrefixEnum.kilo);

            DoTestSerialization(l);

        }

        [Fact]
        public void SerializationTest03()
        {
            Unit km = new Unit(1, 0, 0, 1000, 0);
            
            Length l = new Length(10, km);

            Length l2 = SerializeUnserializeObject(l);

            Assert.Equal(l,l2);

        }

        [Fact]
        public void SerializePhysicalUnits()
        {
            EnergyEquivalent ee = new EnergyEquivalent(10);

            string label = ee.Unit.ToString();

            EnergyEquivalent recreated = SerializeUnserializeObject<EnergyEquivalent>(ee);

            Assert.True(ee.Equals(recreated));
        }

        [Fact]
        public void SerializeEnergyQuantity()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            List<Energy> array =
                new List<Energy>();

            Energy e1 =
                new Energy(300, new WattHour(Unit.SI_PrefixEnum.giga));

            Energy e2 =
                new Energy(200, new WattHour(Unit.SI_PrefixEnum.mega));

            array.Add(e1);
            array.Add(e2);

            string str = JsonConvert.SerializeObject(array, settings);

            string testFile = "energy_test.json";

            File.WriteAllText(testFile, str);

            string inString = File.ReadAllText(testFile);

            List<Energy>? obj = JsonConvert.DeserializeObject<List<Energy>>(inString);

            Assert.True(array.SequenceEqual(obj));


        }

        private static  T SerializeUnserializeObject<T>(T obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            };

            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            };

            serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            TextWriter tw = new StringWriter();
            serializer.Serialize(tw, obj);

            string jsonStr = tw.ToString();

            File.WriteAllText($"unittest{obj.GetType()}.json", jsonStr);

            T recreated = JsonConvert.DeserializeObject<T>(jsonStr, settings);

            tw = new StringWriter();
            serializer.Serialize(tw, recreated);
            string jsonStr2 = tw.ToString();

            File.WriteAllText($"unittest2{obj.GetType()}.json", jsonStr2);

            return recreated;

        }

    }
}

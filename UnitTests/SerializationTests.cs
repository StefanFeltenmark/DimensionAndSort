using System.Collections.Generic;
using System.IO;
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

        [Fact]
        public void SerializationTest02()
        {
            List<Length> lengths = new List<Length>
            {
                new Length(10),
                new Length(20),
                new Length(25)
            };

            var ttest = SerializeUnserializeObject(lengths);

            Assert.Equal(ttest, lengths);

        }

        [Fact]
        public void SerializationTest03()
        {
            List<Energy> objects = new List<Energy>
            {
                new Energy(250000,new WattHour()),
                new Energy(0.25*3600*1000000, new Joule()),
                new Energy(0.25, new MegaWattHour()),
                new Energy(0.25, new WattHour(Unit.SI_PrefixEnum.mega)),
                new Energy(0.25,new WattHour(),Unit.SI_PrefixEnum.mega),
            };

            var ttest = SerializeUnserializeObject(objects);

            Assert.Equal(ttest, objects);

        }

        [Fact]
        public void SerializationTest04()
        {
            var u = (new WattHour(Unit.SI_PrefixEnum.kilo)) / (new Kilogram(Unit.SI_PrefixEnum.kilo)); // kW/ton

            var e = new SpecificEnergy(7560.0, u);

            var u1 = (new Joule(Unit.SI_PrefixEnum.giga)) / (new Kilogram(Unit.SI_PrefixEnum.kilo)); // kW/ton

            SpecificEnergy e2 = e.ConvertToUnit(u1);
           
            var ttest = SerializeUnserializeObject(e2);
           
            Assert.Equal(ttest, e2);
        }

        private T SerializeUnserializeObject<T>(T obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            };

            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            };

            serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            TextWriter tw = new StringWriter();
            serializer.Serialize(tw, obj);

            string? jsonStr = tw.ToString();

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

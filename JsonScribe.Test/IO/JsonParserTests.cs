using JsonScribe.IO;
using NUnit.Framework;

namespace JsonLiteTest.IO
{
    [TestFixture]
    public class JsonParserTests
    {

        [Test]
        [Order(0)]
        public void ParseEmptyObject()
        {
            JsonParser.Parse("{ }");
        }

        [Test]
        [Order(1)]
        public void ParseObjectWithStringProperty()
        {
            JsonParser.Parse(@"{ ""p"": ""value""}");
        }

        [Test]
        [Order(2)]
        public void ParseObjectWithIntProperty()
        {
            JsonParser.Parse(@"{ ""p"": 0 }");
        }

        [Test]
        [Order(3)]
        public void ParseObjectWithDoubleProperty()
        {
            JsonParser.Parse(@"{ ""p"": 1.0 }");
        }

        [Test]
        [Order(4)]
        public void ParseObjectWithNegativeInt()
        {
            JsonParser.Parse(@"{ ""P"": -1 }");
        }

        [Test]
        [Order(5)]
        public void ParseObjectWithNegativeDouble()
        {
            JsonParser.Parse(@"{ ""p"": -1.0 }");
        }
    }
}
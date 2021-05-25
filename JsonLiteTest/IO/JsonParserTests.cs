using JsonLite.IO;
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
            var o = JsonParser.Parse("{ }");
            Assert.Pass();
        }

        [Test]
        [Order(1)]
        public void ParseObjectWithStringProperty()
        {
            var o = JsonParser.Parse(@"{ ""p"": ""value""}");
            Assert.Pass();
        }

        [Test]
        [Order(2)]
        public void ParseObjectWithIntProperty()
        {
            JsonParser.Parse(@"{ ""p"": 0 }");
            Assert.Pass();
        }

        [Test]
        [Order(3)]
        public void ParseObjectWithDoubleProperty()
        {
            JsonParser.Parse(@"{ ""p"": 1.0 }");
            Assert.Pass();
        }

        [Test]
        [Order(4)]
        public void ParseObjectWithNegativeInt()
        {
            JsonParser.Parse(@"{ ""P"": -1 }");
            Assert.Pass();
        }

        [Test]
        [Order(5)]
        public void ParseObjectWithNegativeDouble()
        {
            JsonParser.Parse(@"{ ""p"": -1.0 }");
            Assert.Pass();
        }
    }
}
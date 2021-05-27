using System.Collections.Generic;
using System.Linq;
using JsonLite;
using NUnit.Framework;

namespace JsonLiteTest
{
    [TestFixture]
    public class ExtensionTests
    {

        [Test]
        public void IntToJsonLiteral()
        {
            const int i = 0;
            var literal = i.ToJsonLiteral();
            Assert.AreEqual(literal.AsInt(), i);
        }

        [Test]
        public void IntArrayToJsonArray()
        {
            var a = new[] {1, 2, 3};
            var array = a.ToJsonArray();
            Assert.AreEqual(array.Count, a.Length);
            Assert.IsTrue(a.Contains(((JsonLiteral)array[0]).AsInt()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[1]).AsInt()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[2]).AsInt()));
        }

        [Test]
        public void IntEnumerableToJsonArray()
        {
            var a = new List<int> { 1,2,3};
            var array = a.ToJsonArray();
            Assert.AreEqual(array.Count, a.Count);
            Assert.IsTrue(a.Contains(((JsonLiteral)array[0]).AsInt()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[1]).AsInt()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[2]).AsInt()));
        }
        
        [Test]
        public void ObjectArrayToJsonArray()
        {
            object[] a = {"", 1, 1.1, false};
            var array = a.ToJsonArray();
            Assert.AreEqual(array.Count, a.Length);
            Assert.IsTrue(a.Contains(((JsonLiteral)array[0]).AsString()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[1]).AsInt()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[2]).AsDouble()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[3]).AsBoolean()));
        }

        [Test]
        public void ObjectEnumerableToJsonArray()
        {
            var a = new List<object>{"", 1, 1.1, false};
            var array = a.ToJsonArray();
            Assert.AreEqual(array.Count, a.Count);
            Assert.IsTrue(a.Contains(((JsonLiteral)array[0]).AsString()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[1]).AsInt()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[2]).AsDouble()));
            Assert.IsTrue(a.Contains(((JsonLiteral)array[3]).AsBoolean()));
        }

        [Test]
        public void StringToJsonLiteral()
        {
            const string s = "a";
            var literal = s.ToJsonLiteral();
            Assert.AreEqual(literal.AsString(), s);
        }

        [Test]
        public void StringArrayToJsonArray()
        {
            var s = new[] {"a", "b", "c" };
            var array = s.ToJsonArray();
            Assert.AreEqual(array.Count, s.Length);
            Assert.IsTrue(s.Contains(((JsonLiteral)array[0]).AsString()));
            Assert.IsTrue(s.Contains(((JsonLiteral)array[1]).AsString()));
            Assert.IsTrue(s.Contains(((JsonLiteral)array[2]).AsString()));
        }

        [Test]
        public void StringEnumerableToJsonArray()
        {
            var s = new List<string> {"a", "b", "c" };
            var array = s.ToJsonArray();
            Assert.AreEqual(array.Count, s.Count);
            Assert.IsTrue(s.Contains(((JsonLiteral)array[0]).AsString()));
            Assert.IsTrue(s.Contains(((JsonLiteral)array[1]).AsString()));
            Assert.IsTrue(s.Contains(((JsonLiteral)array[2]).AsString()));
        }
        
        [Test]
        public void DoubleToJsonLiteral()
        {
            const double d = 0.1;
            var literal = d.ToJsonLiteral();
            Assert.AreEqual(literal.AsDouble(), d);
        }

        [Test]
        public void DoubleArrayToJsonArray()
        {
            var d = new[] {0.0, 1.1, 2.2};
            var array = d.ToJsonArray();
            Assert.IsTrue(d.Contains(((JsonLiteral)array[0]).AsDouble()));
            Assert.IsTrue(d.Contains(((JsonLiteral)array[1]).AsDouble()));
            Assert.IsTrue(d.Contains(((JsonLiteral)array[2]).AsDouble()));
        }

        [Test]
        public void DoubleEnumerableToJsonArray()
        {
            var d = new List<double> {0.0, 1.1, 2.2};
            var array = d.ToJsonArray();
            Assert.IsTrue(d.Contains(((JsonLiteral)array[0]).AsDouble()));
            Assert.IsTrue(d.Contains(((JsonLiteral)array[1]).AsDouble()));
            Assert.IsTrue(d.Contains(((JsonLiteral)array[2]).AsDouble()));
        }

        [Test]
        public void BoolToJsonLiteral()
        {
            const bool b = false;
            var literal = b.ToJsonLiteral();
            Assert.AreEqual(b, literal.AsBoolean());
        }

        [Test]
        public void BoolArrayToJsonArray()
        {
            var b = new[] {false, true};
            var array = b.ToJsonArray();
            Assert.IsTrue(b.Contains(((JsonLiteral)array[0]).AsBoolean()));
            Assert.IsTrue(b.Contains(((JsonLiteral)array[1]).AsBoolean()));
        }

        [Test]
        public void BoolEnumerableToJsonArray()
        {
            var b = new List<bool> {false, true};
            var array = b.ToJsonArray();
            Assert.IsTrue(b.Contains(((JsonLiteral)array[0]).AsBoolean()));
            Assert.IsTrue(b.Contains(((JsonLiteral)array[1]).AsBoolean()));
        }
    }
}
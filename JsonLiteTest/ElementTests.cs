using System;
using JsonLite;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace JsonLiteTest
{
    [TestFixture]
    public class ElementTests
    {
        [TestFixture]
        public class CreateLiteralTests
        {
            private void AssertType(JsonLiteral literal, JsonType actual)
            {
                Assert.AreEqual(literal.Type,actual);
            }

            private void AssertNumberTtype(JsonLiteral literal, NumberType actual)
            {
                Assert.AreEqual(literal.NumberType, actual);
            }

            [Test(Description = "Should create int literal")]
            public void CreateIntLiteral()
            {
                var literal = new JsonLiteral(1);
                AssertType(literal, JsonType.Number);
                AssertNumberTtype(literal, NumberType.Integer);
            }
            
            [Test(Description = "Should create double literal")]
            public void CreateDoubleLiteral()
            {
                var literal = new JsonLiteral(1.1);
                AssertType(literal, JsonType.Number);
                AssertNumberTtype(literal, NumberType.Double);
            }
            
            [Test(Description = "Should create string literal")]
            public void CreateStringLiteral()
            {
                var literal = new JsonLiteral("string");
                AssertType(literal, JsonType.String);
            }
            
            [Test(Description = "Should create boolean literal")]
            public void CreateBooleanLiteral()
            {
                var literal = new JsonLiteral(true);
                AssertType(literal, JsonType.Boolean);
            }

            [Test]
            public void ThrowForInvalidLiteralType()
            {
                Assert.Throws<JsonException>(() => new JsonLiteral(new JsonObject()));
            }

            [Test]
            public void ThrowForNullLiteralType()
            {
                Assert.Throws<JsonException>(() => new JsonLiteral(null));
            }

            [Test]
            public void ThrowForNullLiteralObject()
            {
                object v = null;
                Assert.Throws<JsonException>(() => new JsonLiteral(v));
            }

            [TestCase(0, TestName = "Create JSON literal from int object")]
            [TestCase(1.1, TestName = "Create JSON literal from double object")]
            [TestCase(false, TestName = "Create JSON literal from bool object")]
            [TestCase("s", TestName = "Create JSON literal from string object")]
            public void CreateLiteralFromLiteralAsObject(object v)
            {
                new JsonLiteral(v);
            }

            [TestCase(0, TestName = "JSON literal FromValue int")]
            [TestCase(1.1, TestName = "JSON literal FromValue double")]
            [TestCase(false, TestName = "JSON literal FromValue bool")]
            [TestCase("s", TestName = "JSON literal FromValue string")]
            public void CreateLiteralFromValue(object v)
            {
                JsonLiteral.FromValue(v);
            }

            [TestCase(0, TestName = "JSON literal ToString int")]
            [TestCase(1.1, TestName = "JSON literal ToString double")]
            [TestCase(false, TestName = "JSON literal ToString bool")]
            [TestCase("s", TestName = "JSON literal ToString string")]
            public void LiteralToStringMethod(object l)
            {
                var lit = new JsonLiteral(l);
                if (l is bool)
                {
                    Assert.AreEqual(lit.ToString(), l.ToString().ToLower());
                }
                else
                {
                    Assert.AreEqual(lit.ToString(), l.ToString());
                }
            }
            
        }

        [TestFixture]
        public class LiteralToStringTests
        {
            [Test]
            public void LiteralStringToString()
            {
                var literal = new JsonLiteral("String Value");
                Assert.AreEqual(literal.ToString() , "String Value");
            }

            [Test]
            public void LiteralIntToString()
            {
                var literal = new JsonLiteral(1);
                Assert.AreEqual(literal.ToString(), "1");
            }

            [Test]
            public void LiteralDoubleToString()
            {
                var literal = new JsonLiteral(1.1);
                Assert.AreEqual(literal.ToString(), "1.1");
            }

            [Test]
            public void LiteralBooleanToString()
            {
                var literal = new JsonLiteral(false);
                Assert.AreEqual(literal.ToString(), "false");
            }

            [Test]
            public void JsonLiteralEqualComparator()
            {
                var e1 = new JsonLiteral(1);
                var e2 = new JsonLiteral(1);
                Assert.IsTrue(e1 == e2);
                e2 = null;
                Assert.IsFalse(e1 == e2);
                e1 = null;
                Assert.IsTrue(e1 == e2);
            }
            
            [Test]
            public void JsonLiteralEqualMethod()
            {
                var e1 = new JsonLiteral(1);
                Assert.IsTrue(e1.Equals(e1));
                var e2 = new JsonLiteral(1);
                Assert.IsTrue(e1.Equals(e2));
            }

        }

        [TestFixture]
        public class JsonArrayTests
        {

            [Test]
            public void CreateEmptyArray()
            {
                var array = new JsonArray();
                Assert.Zero(array.Count);
            }

            [Test]
            public void EmptyArrayToString()
            {
                var array = new JsonArray();
                var str = array.ToString();
                Assert.AreEqual(str, "[]");
            }

            [Test]
            public void EmptyArrayToStringPretty()
            {
                var array = new JsonArray();
                var str = array.ToString(true);
                Assert.AreEqual(str, "[ ]");
            }

            [TestCase(0,TestName = "Create array single int")]
            [TestCase(1,2,TestName = "Create array two ints")]
            [TestCase("a", TestName = "Create array single string")]
            [TestCase("a", "b", TestName = "Create array two strings")]
            [TestCase(false, TestName = "Create array single bool")]
            [TestCase(false, true, TestName = "Create array two bool")]
            [TestCase(1.1, TestName = "Create array single double")]
            [TestCase(1.1, 2.00, TestName = "Create array two doubles")]
            public void CreateArrayWithLiteralValues(params object[] values)
            {
                var array = new JsonArray(values);
                Assert.AreEqual(array.Count, values.Length);
            }

            [Test]
            public void CreateArrayWithMixedNull()
            {
                object[] a = {null, false, 1, null};
                var array = new JsonArray(a);
                Assert.AreEqual(array.Count, 2);
            }

            [Test]
            public void CreateArrayWithJsonElements()
            {
                JsonElement[] e = {new JsonLiteral(1), new JsonLiteral(1.1), new JsonLiteral("a")};
                var array = new JsonArray(e);
                Assert.AreEqual(array.Count, 3);
            }

            [Test]
            public void ClearArray()
            {
                var array = new JsonArray(new []{1, 2, 3});
                Assert.AreEqual(array.Count, 3);
                array.Clear();
                Assert.IsTrue(array.IsEmpty());
            }

            [Test]
            public void ArrayContainsJsonElement()
            {
                var e = new JsonLiteral(1);
                var array = new JsonArray(e);
                Assert.IsTrue(array.Contains(e));
            }

            [Test]
            public void ArrayContainsJsonElementWithSameValue()
            {
                var e1 = new JsonLiteral(1);
                var e2 = new JsonLiteral(1);
                var array = new JsonArray(e1);
                Assert.IsTrue(array.Contains(e2));
            }

            [Test]
            public void ArrayDoesNotContainJsonElement()
            {
                
            }
            
            [Test]
            public void AddValuesToArray([Values(1, 1.1, "s", false)] object value)
            {
                var array = new JsonArray();
                array.Add(new JsonLiteral(value));
                var lit = array[0];
                Assert.AreEqual(((JsonLiteral)lit).AsValue(), value);
            }

            [Test]
            public void RemoveFromJsonArray()
            {
                var array = new JsonArray();
                array.Add(new JsonLiteral(1));
                Assert.IsFalse(array.IsEmpty());
                var element = array.Remove(0);
                Assert.IsTrue(array.IsEmpty());
                Assert.AreEqual(((JsonLiteral)element).AsInt(), 1);
            }

            [Test]
            public void RemoveFromArrayOutOfIndex()
            {
                var array = new JsonArray();
                array.Add(new JsonLiteral(1));
                Assert.Throws<JsonException>(() => array.Remove(3));
            }

            [TestCase(1,"a",false,2.2)]
            public void CreateArrayWithMixedLiteralValues(params object[] values)
            {
                var array = new JsonArray(values);
                Assert.AreEqual(array.Count, values.Length);
                Assert.AreEqual(((JsonLiteral)array[0]).AsInt(),1);
                Assert.AreEqual(((JsonLiteral)array[1]).AsString(),"a");
                Assert.AreEqual(((JsonLiteral)array[2]).AsBoolean(),false);
                Assert.AreEqual(((JsonLiteral)array[3]).AsDouble(),2.2);
            }

            [Test]
            public void CreateArrayWithJsonObject()
            {
                var obj = new JsonObject();
                var array = new JsonArray(obj);
                Assert.AreSame(array[0],obj);
            }
        }
        
        [TestFixture]
        public class JsonObjectTests
        {

            [Test]
            public void ObjectToString()
            {
                var o = new JsonObject
                {
                    ["p1"] = 3.ToJsonLiteral(),
                    ["p2"] = 1.1.ToJsonLiteral(),
                    ["p3"] = "value".ToJsonLiteral(),
                    ["p4"] = false.ToJsonLiteral()
                };
                var str = o.ToString();
                const string shouldBe = @"{""p1"":3,""p2"":1.1,""p3"":""value"",""p4"":false}";
                Assert.AreEqual(str, shouldBe);
            } 
            
            [Test, Pairwise]
            public void CreateObjectWithValues(
                [Values("p")] string key,
                [Values(1, "v", 2.1, false)] object value)
            {
                var obj = new JsonObject();
                obj[key] = new JsonLiteral(value);
                Assert.AreEqual(value, ((JsonLiteral)obj[key]).AsValue());
            }
        }
    }
}
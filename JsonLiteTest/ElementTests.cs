using JsonLite;
using NUnit.Framework;

namespace JsonLiteTest
{
    [TestFixture]
    public class ElementTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [TestFixture]
        public class CreateLiteralTests
        {
            private void AssertType(JsonLiteral literal, JsonType actual)
            {
                Assert.AreEqual(literal.JsonType,actual);
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
                Assert.Pass();
            }
            
            [Test(Description = "Should create double literal")]
            public void CreateDoubleLiteral()
            {
                var literal = new JsonLiteral(1.1);
                AssertType(literal, JsonType.Number);
                AssertNumberTtype(literal, NumberType.Double);
                Assert.Pass();
            }
            
            [Test(Description = "Should create string literal")]
            public void CreateStringLiteral()
            {
                var literal = new JsonLiteral("string");
                AssertType(literal, JsonType.String);
                Assert.Pass();
            }

            [Test(Description = "Should create null/empty literal")]
            public void CreateNullEmptyLiteral()
            {
                var literal = new JsonLiteral();
                AssertType(literal, JsonType.Null);
                Assert.IsNull(literal.AsValue());
                Assert.Pass();
            }
            
            [Test(Description = "Should create boolean literal")]
            public void CreateBooleanLiteral()
            {
                var literal = new JsonLiteral(true);
                AssertType(literal, JsonType.Boolean);
                Assert.Pass();
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
            public void LiteralNullEmptyToString()
            {
                var literal = new JsonLiteral();
                Assert.AreEqual(literal.ToString(), "null");
            }

        }
    }
}
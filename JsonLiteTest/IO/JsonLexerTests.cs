using JsonLite.IO;
using NUnit.Framework;

namespace JsonLiteTest.IO
{
    public class JsonLexerTests
    {

        private Token[] DoTokenize(string json)
        {
            var lexer = new JsonLexer(json);
            return lexer.Tokenize();
        }
        
        [Test]
        [Order(0)]
        public void LexEmptyObject()
        {
            var tokens = DoTokenize("{ }");
            Assert.IsTrue(tokens.Length == 3);
            Assert.IsTrue(tokens[0].TokenType == TokenType.CurlyOpen && tokens[0].Symbol == "{");
            Assert.IsTrue(tokens[1].TokenType == TokenType.Whitespace && tokens[1].Symbol == " ");
            Assert.IsTrue(tokens[2].TokenType == TokenType.CurlyClose && tokens[2].Symbol == "}");
        }

        [Test]
        [Order(1)]
        public void LexNullProp()
        {
            var tokens = DoTokenize("{ \"p\": null }");
            Assert.IsTrue(tokens.Length == 10);
        }

        [Test]
        [Order(2)]
        public void LexWithNewLine()
        {
            var tokens = DoTokenize("{ \"p\": 1, \n \"a\": null }");
            Assert.IsTrue(tokens.Length == 20);
        }

        [Test]
        [Order(3)]
        public void LexAllLiteralProps()
        {
            var tokens = DoTokenize(
                @"{ ""int"": 1, ""string"": ""Hallo"", ""double"": 1.1, ""bool"", false }"
            );
            Assert.IsTrue(tokens.Length == 38);
        }
    }
}
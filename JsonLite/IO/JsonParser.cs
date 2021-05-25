using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JsonLiteTest")]
namespace JsonLite.IO
{
    public static class JsonParser
    {
        private static Token[] _tokens;
        private static Token _current;
        private static int _index;
        private static int _len;
        
        public static JsonObject Parse(string jsonString)
        {
            try
            {
                var lexer = new JsonLexer(jsonString);
                _tokens = lexer.Tokenize();
                _len = _tokens.Length;
                _index = -1;
                var jsonObject = DoParse();
                return jsonObject;
            }
            catch (JsonException)
            {
                _tokens = null;
                throw;
            }
        }

        private static JsonObject DoParse()
        {
            Next();
            if (_current.TokenType != TokenType.CurlyOpen)
                throw new JsonParseException("JSON document must start with a '{'");
            return ParseObject();
        }

        private static JsonObject ParseObject()
        {
            Next();
            var o = new JsonObject();
            while (_current.TokenType != TokenType.CurlyClose)
            {
                ParseKey(o);
            }
            return o;
        }

        private static void ParseKey(JsonObject o)
        {
            if (_current.TokenType != TokenType.Quote) throw new JsonParseException("Property key's must begin with quote.");
            Next();
            if (_current.TokenType != TokenType.Word) throw new JsonParseException(("Propery key's cannot be empty."));
            var p = _current.Symbol;
            Next();
            if (_current.TokenType != TokenType.Quote) throw new JsonException(("Invalid property definition."));
            Next();
            if (_current.TokenType != TokenType.Colon) throw new JsonException("Colon must follow a key.");
            o[p] = ParseValue();
        }

        private static JsonElement ParseValue()
        {
            Next();
            if(_current.TokenType == TokenType.Quote)
            {
                Next();
                string v = "";
                if (_current.TokenType == TokenType.Word)
                {
                    v = _current.Symbol;
                    Next();
                } 
                if (_current.TokenType != TokenType.Quote)
                {
                    throw new JsonParseException("");
                }
                Next();
                return new JsonLiteral(v);
            }
            if (_current.TokenType == TokenType.Number)
            {
                JsonLiteral literal;
                var sb = new StringBuilder();
                sb.Append(_current.Symbol);
                Next();
                if (_current.TokenType == TokenType.Decimal)
                {
                    sb.Append(_current.Symbol);
                    Next();
                    if (_current.TokenType != TokenType.Number && _current.TokenType != TokenType.Zero) throw new JsonParseException("Number after decimal place required.");
                    sb.Append(_current.Symbol);
                    literal = new JsonLiteral(double.Parse(sb.ToString()));
                }
                else
                {
                    literal = new JsonLiteral(int.Parse(sb.ToString()));
                }

                Next();
                return literal;
            }

            return null;
        }
        

        private static Token LookAhead()
        {
            if (_index + 1 == _len) throw new JsonParseException("");
            return _tokens[_index + 1];
        }

        private static bool LookAheadTypeAny(params TokenType[] types)
        {
            return types.Contains(LookAhead().TokenType);
        }
        
        private static bool LookAheadType(TokenType type)
        {
            return _tokens[_index + 1].TokenType == type;
        }

        private static bool Next()
        {
            if (_index == _len-1) return false;
            _current = _tokens[++_index];
            return true;
        }
    }
}
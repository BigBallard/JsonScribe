using System;
using System.Linq;

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
            if (LookAheadType(TokenType.Whitespace))
            {
                
            }
        }

        private static Token? LookAhead()
        {
            if (_index + 1 == _len) return null;
            return _tokens[_index + 1];
        }

        private static bool LookAheadTypeAny(params TokenType[] types)
        {
            var la = LookAhead();
            return la != null && types.Contains(la.Value.TokenType);
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
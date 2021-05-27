using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JsonScribe.Test")]
namespace JsonScribe.IO
{
    internal class JsonLexer
    {
        private string _symbols;
        private int _len;
        private char _current = '\0';
        private int _index = -1;
        private readonly List<Token> _tokens = new List<Token>();

        private readonly char[] _unicodeChars = {'a', 'b', 'c', 'd', 'e', 'f', 'g', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        
        internal JsonLexer(string jsonString) //Ref safe due to non-altering operations;
        {
            _symbols = jsonString;
            _len = _symbols.Length;
        }

        private bool Next()
        {
            if (_index == _len-1) return false;
            _current = _symbols[++_index];
            return true;
        }

        public Token[] Tokenize()
        {
            while (Next())
            {
                if (Is('{')) Push(TokenType.CurlyOpen);
                else if (Is('}')) Push(TokenType.CurlyClose);
                else if (char.IsWhiteSpace(_current)) continue;
                else if (Is('"')) Push(TokenType.Quote);
                else if (Is('.')) Push(TokenType.Decimal);
                else if (Is(',')) Push(TokenType.Comma);
                else if (char.IsLetter(_current)) DoWord();
                else if (Is('0')) DoZero();
                else if (Is('-')) DoNumber();
                else if (char.IsDigit(_current)) DoNumber();
                else if (Is(':')) Push(TokenType.Colon);
                else if (Is('\\')) DoEscaped();
                else throw new JsonParseException($"Unexpected start of token '{_current}'.");
            }

            return _tokens.ToArray();
        }

        private bool Is(char value)
        {
            return _current == value;
        }

        private bool IsAny(char[] others)
        {
            return others.Contains(_current);
        }
        
        private void DoWord()
        {
            var sb = new StringBuilder();
            sb.Append(_current);
            while (!LookAheadWhitespace() && !LookAheadQuote())
            {
                Next();
                sb.Append(_current);
            }
            Push(TokenType.Word, sb.ToString());
        }

        private void DoZero()
        {
            if (LookAheadDigit()) throw new JsonParseException("Numbers cannot lead with '0'.");
            if (Is('\\'))
            {
                DoUnicode();
            }
            else
            {
                Push(TokenType.Number, "0");
            }
        }

        private void DoNumber()
        {
            var sb = new StringBuilder();
            sb.Append(_current);
            while (LookAheadDigit())
            {
                Next();
                sb.Append(_current);
            }
            Push(TokenType.Number, sb.ToString());
        }

        private void DoEscaped()
        {
            if (LookAheadWhitespace()) return;
            if (LookAhead() == 'u')
            {
                Next();
                var sb = new StringBuilder();
                sb.Append("\\u");
                var uniCount = 0;
                while (uniCount < 4 && Next())
                {
                    uniCount++;
                    if (!IsAny(_unicodeChars))
                        throw new JsonParseException($"Invalid unicode sequence at {_index}: {_current}");
                    sb.Append(_current);
                }
                Push(TokenType.Unicode, sb.ToString());
            }
            else
            {
                throw new JsonParseException($"Invalid escaped sequence after index {_index}: \\{LookAhead()}");
            }
        }

        private void DoUnicode()
        {
            var aheads = 0;
            var sb = new StringBuilder();
            while (aheads != 4)
            {
                if (LookAheadDigit() || LookAheadAlpha())
                {
                    Next();
                    sb.Append(_current);
                    aheads++;
                }
                else
                {
                    throw new JsonParseException($"Invalid unicode sequence detected at index {_index}");
                }
            }
            Push(TokenType.Unicode, sb.ToString());
        }

        private char LookAhead()
        {
            var lai = _index + 1;
            if (lai == _len) throw new JsonParseException();
            return _symbols[lai];
        }

        private bool LookAheadAlpha()
        {
            return char.IsLetter(LookAhead());
        }
        
        private bool LookAheadFor(char c)
        {
            return LookAhead() == c;
        }

        private bool LookAheadAny(char[] c)
        {
            return c.Contains(LookAhead());
        }
        
        private bool LookAheadDigit()
        {
            return char.IsDigit(LookAhead());
        }

        private bool LookAheadDecimal()
        {
            return LookAhead() == '.';
        }

        private bool LookAheadWhitespace()
        {
            var ahead = LookAhead();
            return (_current == '\\' && 
                   (ahead == 'n' || ahead == 't' || ahead == 'r' || ahead == 'f')) ||
                   ahead == ' ';
        }

        private bool LookAheadQuote()
        {
            return LookAhead() == '"';
        }
        
        private void Push(TokenType type)
        {
            _tokens.Add(new Token{TokenType = type, Symbol = _current.ToString()});
        }

        private void Push(TokenType type, string symbols)
        {
            _tokens.Add(new Token{TokenType = type, Symbol = symbols});
        }
    }
}
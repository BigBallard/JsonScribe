using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JsonLiteTest")]
namespace JsonLite.IO
{
    internal class JsonLexer
    {
        private string _symbols;
        private int _len;
        private char _current = '\0';
        private int _index = -1;
        private readonly List<Token> _tokens = new List<Token>();
        private readonly char[] _escapeSingleChars = new[] {'n', 'r', 'f', 't', 'b', '\\', '/', '*'};
        
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
                else if (char.IsWhiteSpace(_current)) Push(TokenType.Whitespace);
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
            Push(TokenType.Zero);
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
            if (LookAheadAny(_escapeSingleChars))
            {
                Next();
                Push(TokenType.Escaped, "\\" + _current);
            }else if (LookAheadFor('u'))
            {
                DoUnicode();
            }
            throw new JsonParseException($"Invalid escaped sequence after index {_index}: \\{LookAhead()}");
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
            return char.IsWhiteSpace(LookAhead());
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
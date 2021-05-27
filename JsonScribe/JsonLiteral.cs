using System.Globalization;

namespace JsonScribe
{
    public sealed class JsonLiteral: JsonElement
    {
        private object _literal;
        public NumberType NumberType { get; private set; } = NumberType.None;
        
        private void Init(object literal, JsonType type, NumberType numType = NumberType.None)
        {
            _literal = literal ?? throw new JsonException("Literal cannot be null.");
            Type = type;
            NumberType = numType;
        }

        public static JsonLiteral FromValue(object value)
        {
            return new JsonLiteral(value);
        }

        public JsonLiteral(object value)
        {
            switch (value)
            {
                case int i:
                    Init(i, JsonType.Number, NumberType.Integer);
                    break;
                case double d:
                    Init(d, JsonType.Number, NumberType.Double);
                    break;
                case string s:
                    Init(s, JsonType.String);
                    break;
                case bool b:
                    Init(b, JsonType.Boolean);
                    break;
                default:
                    throw new JsonException("object value is not a valid JSON literal.");
            }
        }

        public JsonLiteral(string value) => Init(value, JsonType.String);

        public JsonLiteral(int value) => Init(value, JsonType.Number, NumberType.Integer);

        public JsonLiteral(double value) => Init(value, JsonType.Number, NumberType.Double);

        public JsonLiteral(bool value) => Init(value, JsonType.Boolean);

        public string AsString() => (string) _literal;

        public int AsInt() => (int) _literal;

        public double AsDouble() => (double) _literal;

        public bool AsBoolean() => (bool) _literal;

        public object AsValue() => _literal;

        public override string ToString()
        {
            if(Type == JsonType.String) return (string)_literal;
            if(Type == JsonType.Boolean) return ((bool) _literal).ToString().ToLower();
            if(NumberType == NumberType.Integer) return ((int) _literal).ToString();
            return ((double) _literal).ToString(CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
             if (!(obj is JsonLiteral other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.Type != Type) return false;
            if (other.Type != JsonType.Number || Type != JsonType.Number) return other.AsValue() != _literal;
            if(other.NumberType != NumberType) return false;
            return other._literal != _literal;
        }
        
    }
}
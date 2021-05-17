using System;
using System.Globalization;

namespace JsonLite
{
    public sealed class JsonLiteral: JsonElement
    {
        private object _literal;
        public JsonType JsonType { get; private set; } = JsonType.Null;
        public NumberType NumberType { get; private set; } = NumberType.None;

        public JsonLiteral()
        {
        }
        
        private void Init(object literal, JsonType type, NumberType numType = NumberType.None)
        {
            _literal = literal ?? throw new JsonException("Literal cannot be null.");
            JsonType = type;
            NumberType = numType;
        }

        public JsonLiteral(string value) => Init(value, JsonType.String);

        public JsonLiteral(int value) => Init(value, JsonType.Number, NumberType.Integer);

        public JsonLiteral(double value) => Init(value, JsonType.Number, NumberType.Double);

        public JsonLiteral(bool value) => Init(value, JsonType.Boolean);

        public string AsString() => (string) _literal;

        public int AsInt() => (int) _literal;

        public long AsLong() => (long) _literal;

        public double AsDouble() => (double) _literal;

        public bool AsBoolean() => (bool) _literal;

        public object AsValue() => _literal;

        public override string ToString()
        {
            switch (JsonType)
            {
                case JsonType.Object:
                    return ((JsonObject) _literal).ToString();
                case JsonType.Array:
                    return ((JsonArray) _literal).ToString();
                case JsonType.String:
                    return (string)_literal;
                case JsonType.Boolean:
                    return ((bool) _literal).ToString().ToLower();
                case JsonType.Null:
                    return "null";
                case JsonType.Number:
                    switch (NumberType)
                    {
                        case NumberType.None:
                            return null;
                        case NumberType.Integer:
                            return ((int) _literal).ToString();
                        case NumberType.Double:
                            return ((double) _literal).ToString(CultureInfo.InvariantCulture);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using JsonLite.IO;

namespace JsonLite
{
    public sealed class JsonObject: JsonElement
    {
        private readonly Dictionary<string, JsonElement> _members;

        public JsonObject()
        {
            _members = new Dictionary<string, JsonElement>();
            Type = JsonType.Object;
        }

        public bool IsEmpty() => _members.Count == 0;

        public JsonElement this[string property]
        {
            get => _members.ContainsKey(property) ? _members[property] : null;
            set => _members[property] = value;
        }

        public void Set(string property, JsonElement value)
        {
            this[property] = value;
        }

        public void SetString(string property, string value)
        {
            Set(property, new JsonLiteral(value));
        }

        public void SetInt(string property, int value)
        {
            Set(property, new JsonLiteral(value));
        }

        public void SetFloat(string property, float value)
        {
            Set(property, new JsonLiteral(value));
        }

        public void SetBool(string property, bool value)
        {
            Set(property, new JsonLiteral(value));
        }

        public void SetObject(string property, JsonObject value)
        {
            Set(property, value);
        }

        public void SetArray(string property, JsonArray value)
        {
            Set(property, value);
        }

        public List<string> Keys()
        {
            return _members.Keys.ToList();
        }

        public override string ToString()
        {
            var writer = new JsonWriter();
            return writer.Write(this);
        }

        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
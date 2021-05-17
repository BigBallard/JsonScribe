using System.Collections.Generic;

namespace JsonLite
{
    public sealed class JsonArray: JsonElement
    {
        private List<JsonElement> _elements;

        public JsonArray()
        {
            _elements = new List<JsonElement>();
            Type = JsonType.Array;
        }

        public int Count => _elements.Count;

        public JsonElement this[int index] =>
            index < Count
                ? _elements[index]
                : throw new JsonException($"Index {index} is out of range of {Count}");

        public void Add(JsonElement element)
        {
            _elements.Add(element);
        }

        public JsonElement Remove(int index)
        {
            if (index >= Count) throw new JsonException($"Index {index} is out of range of {Count}");
            var element = _elements[index];
            _elements.Remove(element);
            return element;
        }

        public void Clear() => _elements.Clear();

        public bool Contains(JsonElement element)
        {
            return _elements.Contains(element);
        }
    }
}
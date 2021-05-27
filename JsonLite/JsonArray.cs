using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JsonLite
{
    public sealed class JsonArray: JsonElement, IEnumerable<JsonElement>
    {
        private List<JsonElement> _elements = new List<JsonElement>();

        public JsonArray()
        {
            Type = JsonType.Array;
        }

        public JsonArray(IEnumerable<object> values): this()
        {
            foreach (var value in values)
            {
                switch (value)
                {
                    case null:
                        continue;
                    case JsonElement j:
                        _elements.Add(j);
                        break;
                    default:
                        _elements.Add(new JsonLiteral(value));
                        break;
                }
            }
        }

        public JsonArray(JsonElement value)
        {
            if(value != null) _elements.Add(value);
        }
        
        public JsonArray(IEnumerable<int> values): this()
        {
            foreach (var value in values) _elements.Add(new JsonLiteral(value));
        }
        
        public JsonArray(IEnumerable<bool> values): this()
        {
            foreach (var value in values) _elements.Add(new JsonLiteral(value));
        }
        
        public JsonArray(IEnumerable<double> values): this()
        {
            foreach (var value in values) _elements.Add(new JsonLiteral(value));
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

        public bool IsEmpty()
        {
            return _elements.Count == 0;
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
            return element != null && _elements.Any(e => e.Equals(element));
        }

        public string ToString(bool pretty = false)
        {
            if (pretty) return ToStringPretty();
            return "[]";
        }
        
        private string ToStringPretty()
        {
            return "[ ]";
        }

        public IEnumerator<JsonElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class JsonArrayEnumerator : IEnumerator<JsonElement>
        {
            public bool MoveNext()
            {
                throw new System.NotImplementedException();
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }

            public JsonElement Current { get; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
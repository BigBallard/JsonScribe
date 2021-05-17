namespace JsonLite
{
    public abstract class JsonElement
    {
        public JsonType Type { get; protected set; } = JsonType.Null;

    }
}
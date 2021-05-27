namespace JsonScribe
{
    public abstract class JsonElement
    {
        public JsonType Type { get; protected set; }

        public new abstract bool Equals(object obj);

        public static bool operator ==(JsonElement e1, JsonElement e2)
        {
            if (e1 is { }) return e1.Equals(e2);
            return e2 is null;
        }

        public static bool operator !=(JsonElement e1, JsonElement e2)
        {
            return !(e1 == e2);
        }
    }
}
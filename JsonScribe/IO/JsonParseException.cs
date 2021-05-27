using System;
using System.Runtime.Serialization;

namespace JsonScribe.IO
{
    public class JsonParseException: JsonException
    {
        public JsonParseException()
        {
        }

        protected JsonParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JsonParseException(string message) : base(message)
        {
        }

        public JsonParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
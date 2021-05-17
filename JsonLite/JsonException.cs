using System;
using System.Runtime.Serialization;

namespace JsonLite
{
    public class JsonException: Exception
    {
        public JsonException()
        {
        }

        protected JsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JsonException(string message) : base(message)
        {
        }

        public JsonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
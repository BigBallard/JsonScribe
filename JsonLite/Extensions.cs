using System.Collections.Generic;
using System.Linq;

namespace JsonLite
{
    public static class Extensions
    {
        public static JsonLiteral ToJsonLiteral(this string value) => InternalToJsonLiteral(value);
        public static JsonArray ToJsonArray(this string[] value) => new JsonArray(value);
        public static JsonArray ToJsonArray(this IEnumerable<string> value) => new JsonArray(value);
        
        public static JsonLiteral ToJsonLiteral(this int value) => InternalToJsonLiteral(value);
        public static JsonArray ToJsonArray(this int[] value) => new JsonArray(value);
        public static JsonArray ToJsonArray(this IEnumerable<int> value) => new JsonArray(value);
        
        public static JsonLiteral ToJsonLiteral(this bool value) => InternalToJsonLiteral(value);
        public static JsonArray ToJsonArray(this bool[] value) => new JsonArray(value);
        public static JsonArray ToJsonArray(this IEnumerable<bool> value) => new JsonArray(value);
        
        public static JsonLiteral ToJsonLiteral(this double value) => InternalToJsonLiteral(value);
        public static JsonArray ToJsonArray(this double[] value) => new JsonArray(value);
        public static JsonArray ToJsonArray(this IEnumerable<double> value) => new JsonArray(value);

        public static JsonArray ToJsonArray(this object[] value) => new JsonArray(value);
        public static JsonArray ToJsonArray(this IEnumerable<object> value) => new JsonArray(value);

        private static JsonLiteral InternalToJsonLiteral(object value) => new JsonLiteral(value);
    }
}
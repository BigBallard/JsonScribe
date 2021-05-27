using System;
using System.Text;

namespace JsonLite.IO
{
    public class JsonWriter
    {
        public bool PrettyPrint = false;

        public string Write(JsonObject jsonObject)
        {
            var sb = new StringBuilder();
            if (jsonObject == null) return null;
            WriteObject(sb, jsonObject);
            return sb.ToString();
        }

        private void WriteObject(StringBuilder sb, JsonObject o)
        {
            sb.Append("{");
            var keys = o.Keys();
            if (keys.Count > 0)
            {
                foreach (var k in o.Keys())
                {
                    sb.Append("\"").Append(k).Append("\":");
                    var v = o[k];
                    if (v == null)
                    {
                        sb.Append("null");
                    }
                    else if (v is JsonObject o2)
                    {
                        WriteObject(sb, o2);
                    }else if (v is JsonArray a)
                    {
                        WriteArray(sb, a);
                    }
                    else
                    {
                        var t = v.Type;
                        if (t == JsonType.String)
                        {
                            sb.Append("\"").Append(((JsonLiteral)v).AsValue()).Append("\"");
                        }
                        else
                        {
                            sb.Append(((JsonLiteral)v));
                        }
                    }
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");
        }

        private void WriteArray(StringBuilder sb, JsonArray a)
        {
            sb.Append("[");
            if (a.Count > 0)
            {
                for (var i = 0; i < a.Count; i++)
                {
                    var v = a[i];
                    if (v.Type == JsonType.Array)
                    {
                        WriteArray(sb, v as JsonArray);
                    }
                    else if (v.Type == JsonType.Object)
                    {
                        WriteObject(sb, v as JsonObject);
                    }
                    else
                    {
                        sb.Append(((JsonLiteral) v).AsValue());
                    }
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, sb.Length);
            }
            sb.Append("]");
        }
    }
}
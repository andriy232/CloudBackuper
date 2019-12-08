using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Helper
{
    public static class StringExt
    {
        public static string FormatJson(this string result)
        {
            var parsedJson = JToken.Parse(result);
            var beautified = parsedJson.ToString(Formatting.Indented);
            return beautified;
        }
    }
}
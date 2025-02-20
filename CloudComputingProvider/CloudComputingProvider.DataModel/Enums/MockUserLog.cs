using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace CloudComputingProvider.DataModel.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MockUserLog
    {
        ApiUser
    }
}

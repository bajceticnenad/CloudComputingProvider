using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudComputingProvider.BusinessModel.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum State
    {
        Active = 1,
        Inactive = 2,
        OnHold = 3,
        Cancelled = 4
    }
}

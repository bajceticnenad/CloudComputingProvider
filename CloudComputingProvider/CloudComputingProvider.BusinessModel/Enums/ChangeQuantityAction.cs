using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudComputingProvider.BusinessModel.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChangeQuantityAction
    {
        None,
        Added,
        Removed
    }
}

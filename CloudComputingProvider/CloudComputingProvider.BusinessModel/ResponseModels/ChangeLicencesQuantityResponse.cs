using CloudComputingProvider.BusinessModel.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class ChangeLicencesQuantityResponse
    {
        public int LicenceId { get; set; }
        public string Licence { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ChangeQuantityAction Action { get; set; }
    }
}

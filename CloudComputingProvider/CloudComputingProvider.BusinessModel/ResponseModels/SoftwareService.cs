using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class SoftwareService
    {
        public int SoftwareId { get; set; }

        [StringLength(250)]
        public string SoftwareName { get; set; }

        public int Quantity { get; set; }
    }
}

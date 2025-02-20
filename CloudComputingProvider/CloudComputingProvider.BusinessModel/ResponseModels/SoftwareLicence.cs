using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class SoftwareLicence
    {
        public int LicenceId { get; set; }

        [StringLength(250)]
        public string Licence { get; set; }

        public int SoftwareId { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}

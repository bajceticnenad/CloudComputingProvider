using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingProvider.DataModel.Software
{
    public class AddNewSubscriptionLicenceRequest
    {
        public int OrderId { get; set; }
        public int SoftwareId { get; set; }
        public int Quantity { get; set; }
    }
}

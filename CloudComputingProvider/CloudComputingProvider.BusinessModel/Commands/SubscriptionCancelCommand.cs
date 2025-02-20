using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingProvider.BusinessModel.Commands
{
    public class SubscriptionCancelCommand : IRequest<Response>
    {
        public int SubscriptionId { get; set; }
    }
}

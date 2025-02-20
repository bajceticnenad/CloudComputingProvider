using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;

namespace CloudComputingProvider.BusinessModel.Commands
{
    public class CreateOrderCommand : IRequest<Response<Order>>
    {
        public int CustomerAccountId { get; set; }
        public List<SoftwareService> SoftwareServices { get; set; }
    }
}

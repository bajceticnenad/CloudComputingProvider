using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;

namespace CloudComputingProvider.BusinessModel.Queries
{
    public class GetCustomerSoftwareServicesQuery : IRequest<Response<List<SoftwareService>>>
    {
        public int CustomerId { get; set; }
    }
}
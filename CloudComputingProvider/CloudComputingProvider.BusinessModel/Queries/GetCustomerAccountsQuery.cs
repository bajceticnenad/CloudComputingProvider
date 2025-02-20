using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;

namespace CloudComputingProvider.BusinessModel.Queries
{
    public class GetCustomerAccountsQuery : IRequest<Response<List<CustomerAccountsResponse>>>
    {
        public int CustomerId { get; set; }
    }
}

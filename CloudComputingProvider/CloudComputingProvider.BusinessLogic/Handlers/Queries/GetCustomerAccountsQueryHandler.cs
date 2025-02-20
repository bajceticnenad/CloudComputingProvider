using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Queries;
using CloudComputingProvider.BusinessModel.ResponseModels;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.BusinessLogic.Handlers.Queries
{
    public class GetCustomerAccountsQueryHandler : IRequestHandler<GetCustomerAccountsQuery, Response<List<CustomerAccountsResponse>>>
    {
        #region PrivateFields
        private readonly ILogger<GetCustomerAccountsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomersRepository _customersRepository;

        #endregion PrivateFields

        #region Constructor
        public GetCustomerAccountsQueryHandler(ILogger<GetCustomerAccountsQueryHandler> logger, IMapper mapper, ICustomersRepository customersRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _customersRepository = customersRepository;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response<List<CustomerAccountsResponse>>> Handle(GetCustomerAccountsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(GetCustomerAccountsQueryHandler)}/{nameof(Handle)}");

            var response = new Response<List<CustomerAccountsResponse>>()
            {
                Data = new List<CustomerAccountsResponse>(),
                Success = true
            };

            var customerAccounts = await _customersRepository.GetCustomerAccounts(request.CustomerId, cancellationToken);
            if (customerAccounts.Any())
            {
                response.Data = _mapper.Map<List<CustomerAccountsResponse>>(customerAccounts);
            }

            return response;
        }
        #endregion PublicMethods
    }
}

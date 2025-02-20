using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Queries;
using CloudComputingProvider.BusinessModel.ResponseModels;
using CloudComputingProvider.Services.Interfaces.APIs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.BusinessLogic.Handlers.Queries
{
    public class GetCustomerSoftwareServicesHandler : IRequestHandler<GetCustomerSoftwareServicesQuery, Response<List<SoftwareService>>>
    {
        #region PrivateFields
        private readonly ILogger<GetCustomerSoftwareServicesHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICcpOrderService _ccpOrderService;

        #endregion PrivateFields

        #region Constructor
        public GetCustomerSoftwareServicesHandler(ILogger<GetCustomerSoftwareServicesHandler> logger, IMapper mapper, ICcpOrderService ccpOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _ccpOrderService = ccpOrderService;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response<List<SoftwareService>>> Handle(GetCustomerSoftwareServicesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(GetCustomerSoftwareServicesHandler)}/{nameof(Handle)}");

            var response = new Response<List<SoftwareService>>()
            {
                Data = new List<SoftwareService>(),
                Success = true
            };

            var result = await _ccpOrderService.GetAvailableSoftwareServices(request.CustomerId);
            if (!result.Success)
            {
                response.Success = result.Success;
                response.ResponseMessage = result.ResponseMessage;
                return response;
            }

            response.Data = _mapper.Map<List<SoftwareService>>(result.Data);

            return response;
        }
        #endregion PublicMethods
    }
}

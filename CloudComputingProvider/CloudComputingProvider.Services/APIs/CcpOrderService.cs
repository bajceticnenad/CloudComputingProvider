using AutoMapper;
using CloudComputingProvider.DataModel;
using CloudComputingProvider.DataModel.Order;
using CloudComputingProvider.DataModel.Software;
using CloudComputingProvider.Services.Interfaces.APIs;
using CloudComputingProvider.Services.Interfaces.Cache;
using CloudComputingProvider.Services.Interfaces.Mock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.Services.APIs
{
    public class CcpOrderService : ICcpOrderService
    {
        #region PrivateFields
        private readonly string _redisHelthCheckKey;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CcpOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IMockCcpApi _mockCcpApi;
        #endregion PrivateFields

        #region Constructor
        public CcpOrderService(ILogger<CcpOrderService> logger, ICacheService cacheService, IConfiguration configuration,
            IMockCcpApi mockCcpApi, IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _cacheService = cacheService;
            _redisHelthCheckKey = _configuration?.GetSection("Redis")["RedisHelthCheckKey"];
            _mockCcpApi = mockCcpApi;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response<List<SoftwareService>>> GetAvailableSoftwareServices(int customerId)
        {
            var response = new Response<List<SoftwareService>>()
            {
                Success = true
            };

            var result = await _mockCcpApi.GetAvailableSoftwareServices(customerId);
            if (!result.Any())
            {
                response.Success = false;
                response.ResponseMessage = $"Currently, there are no available services for Customer Id = {customerId}";
                return response;
            }

            response.Data = _mapper.Map<List<SoftwareService>>(result);

            return response;
        }

        public async Task<Response<Order>> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var response = new Response<Order>()
            {
                Success = true
            };

            var result = await _mockCcpApi.CreateOrder(createOrderRequest);
            if (result == null)
            {
                response.Success = false;
                response.ResponseMessage = $"The order was not created properly for CustomerAccountId = {createOrderRequest.CustomerAccountId}";
                return response;
            }

            response.Data = _mapper.Map<Order>(result);

            return response;
        }

        public async Task<Response> CancelSubscription(CancelSubscriptionRequest request)
        {
            var response = new Response()
            {
                Success = true
            };

            var result = await _mockCcpApi.CancelSubscription(request);
            if (result == null)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription is not cancelled properly!";
                return response;
            }

            response = _mapper.Map<Response>(result);
            return response;
        }

        public async Task<Response> ExtendLicenceValidDate(ExtendLicenceValidDateRequest request)
        {
            var response = new Response()
            {
                Success = true
            };

            var result = await _mockCcpApi.ExtendLicenceValidDate(request);
            if (result == null)
            {
                response.Success = false;
                response.ResponseMessage = $"Licence Valid To Date is not Extended properly!";
                return response;
            }

            response = _mapper.Map<Response>(result);
            return response;
        }

        public async Task<Response> CancelSubscriptionLicence(CancelSubscriptionLicenceRequest request)
        {
            var response = new Response()
            {
                Success = true
            };

            var result = await _mockCcpApi.CancelSubscriptionLicence(request);
            if (result == null)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription Licence/s is not cancelled properly!";
                return response;
            }

            response = _mapper.Map<Response>(result);
            return response;
        }

        public async Task<Response<List<SoftwareLicence>>> AddNewSubscriptionLicence(AddNewSubscriptionLicenceRequest request)
        {
            var response = new Response<List<SoftwareLicence>>()
            {
                Data = new List<SoftwareLicence>(),
                Success = true
            };

            var result = await _mockCcpApi.AddNewSubscriptionLicence(request);
            if (result == null)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription Licence/s is not added properly!";
                return response;
            }

            response.Data = _mapper.Map<List<SoftwareLicence>>(result);

            return response;
        }
        #endregion PublicMethods
    }
}

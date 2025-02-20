using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Commands;
using CloudComputingProvider.BusinessModel.Queries;
using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CloudComputingProvider.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region PrivateFields
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;
        #endregion PrivateFields

        #region Constructor
        /// <summary>
        /// Order Controller constructor
        /// </summary>
        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion Constructor

        #region PublicMethods
        /// <summary>
        /// Return list of software services available on CCP
        /// </summary>
        [HttpGet("customerId/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<List<SoftwareService>>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCustomerSoftwareServices([FromRoute] int customerId)
        {
            _logger.LogDebug($"Logger Test: {nameof(CustomerController)}/{nameof(GetCustomerSoftwareServices)}");
            var query = new GetCustomerSoftwareServicesQuery
            {
                CustomerId = customerId
            };
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Ordering software license
        /// </summary>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<Order>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            _logger.LogDebug($"Logger Test: {nameof(CustomerController)}/{nameof(CreateOrder)}");
            return Ok(await _mediator.Send(command));
        }
        #endregion PublicMethods
    }
}

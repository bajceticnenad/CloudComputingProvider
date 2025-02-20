using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Queries;
using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CloudComputingProvider.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region PrivateFields
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;
        #endregion PrivateFields

        #region Constructor
        /// <summary>
        /// Customer Controller constructor
        /// </summary>
        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion Constructor

        #region PublicMethods
        /// <summary>
        /// Return Accounts by Customer 
        /// </summary>
        [HttpGet("customerId/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<CustomerAccountsResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCustomerAccounts([FromRoute] int customerId)
        {
            _logger.LogDebug($"Logger Test: {nameof(CustomerController)}/{nameof(GetCustomerAccounts)}");
            var query = new GetCustomerAccountsQuery
            {
                CustomerId = customerId
            };
            return Ok(await _mediator.Send(query));
        }
        #endregion PublicMethods

    }
}

using CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.DeleteExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.UpdateExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestDetail;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoExchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExchangeRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExchangeRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ExchangeRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<ExchangeRequestListDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetExchangeRequestListQuery());
            return Ok(leaveRequests);
        }

        // GET api/<ExchangeRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeRequestDetailsDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetExchangeRequestDetailQuery { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<ExchangeRequestsController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateExchangeRequestCommand exchangeRequest)
        {
            var response = await _mediator.Send(exchangeRequest);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<ExchangeRequestsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateExchangeRequestCommand exchangeRequest)
        {
            await _mediator.Send(exchangeRequest);
            return NoContent();
        }

        // DELETE api/<ExchangeRequestsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteExchangeRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

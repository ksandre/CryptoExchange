using CryptoExchange.Application.Features.ExchangeRequest.Commands.ApproveExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.CreateExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.DeleteExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Commands.UpdateExchangeRequest;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestDetail;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetExchangeRequestList;
using CryptoExchange.Application.Features.ExchangeRequest.Queries.GetRelatedExchangeRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoExchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExchangeRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ExchangeRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<ExchangeRequestListDto>>> Get()
        {
            var exchangeRequest = await _mediator.Send(new GetExchangeRequestListQuery());
            return Ok(exchangeRequest);
        }

        // GET api/<ExchangeRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeRequestDetailsDto>> Get(int id)
        {
            var exchangeRequest = await _mediator.Send(new GetExchangeRequestDetailQuery { Id = id });
            return Ok(exchangeRequest);
        }

        // GET api/<ExchangeRequestsController>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<RelatedExchangeRequestsListDto>>> GetRelatedExchangeRequests(
            [FromQuery] double currencyToExchangeAmount,
            [FromQuery] double currencyForExchangeAmount,
            [FromQuery] int currencyToExchangeId,
            [FromQuery] int currencyForExchangeId
        )
        {
            var exchangeRequests = await _mediator.Send(new GetRelatedExchangeRequestsListQuery {
                CurrencyToExchangeAmount = currencyToExchangeAmount,
                CurrencyForExchangeAmount = currencyForExchangeAmount,
                CurrencyToExchangeId = currencyToExchangeId,
                CurrencyForExchangeId = currencyForExchangeId
            });
            return Ok(exchangeRequests);
        }

        // POST api/<ExchangeRequestsController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateExchangeRequestCommand exchangeRequest)
        {
            var response = await _mediator.Send(exchangeRequest);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ApproveTwoExchangeRequests(ApproveTwoExchangeRequestsCommand approveExchangeRequest)
        {
            var response = await _mediator.Send(approveExchangeRequest);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<ExchangeRequestsController>/5
        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
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

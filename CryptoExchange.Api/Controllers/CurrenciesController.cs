using CryptoExchange.Application.Features.Currency.Commands.CreateCurrency;
using CryptoExchange.Application.Features.Currency.Commands.DeleteCurrency;
using CryptoExchange.Application.Features.Currency.Commands.UpdateCurrency;
using CryptoExchange.Application.Features.Currency.Queries.GetAllCurrencies;
using CryptoExchange.Application.Features.Currency.Queries.GetCurrencyDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoExchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CurrenciesController>
        [HttpGet]
        public async Task<List<CurrencyDto>> Get()
        {
            var currencies = await _mediator.Send(new GetCurrenciesQuery());
            return currencies;
        }

        // GET api/<CurrenciesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyDetailsDto>> Get(int id)
        {
            var currency = await _mediator.Send(new GetCurrencyDetailsQuery(id));
            return Ok(currency);
        }

        // POST api/<CurrenciesController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateCurrencyCommand currency)
        {
            var response = await _mediator.Send(currency);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<CurrenciesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateCurrencyCommand currency)
        {
            await _mediator.Send(currency);
            return NoContent();
        }

        // DELETE api/<CurrenciesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCurrencyCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

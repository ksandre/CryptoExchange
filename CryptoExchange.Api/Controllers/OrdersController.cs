using CryptoExchange.Application.Features.Order.Commands.CreateOrder;
using CryptoExchange.Application.Features.Order.Commands.DeleteOrder;
using CryptoExchange.Application.Features.Order.Commands.UpdateOrder;
using CryptoExchange.Application.Features.Order.Queries.GetOrderDetails;
using CryptoExchange.Application.Features.Order.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoExchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            var orders = await _mediator.Send(new GetOrderListQuery());
            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> Get(int id)
        {
            var order = await _mediator.Send(new GetOrderDetailQuery { Id = id });
            return Ok(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateOrderCommand order)
        {
            var response = await _mediator.Send(order);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<OrdersController>/5
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateOrderCommand order)
        {
            await _mediator.Send(order);
            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

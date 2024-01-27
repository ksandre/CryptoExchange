using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CryptoExchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUserService _userService;

        public CustomersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<List<Customer>> GetAsync()
        {
            var customers = await _userService.GetCustomers();

            return customers;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<Customer> Get(string id)
        {
            var customer = await _userService.GetCustomer(id);

            return customer;
        }
    }
}

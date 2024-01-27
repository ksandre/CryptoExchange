using CryptoExchange.Application.Models.Identity;

namespace CryptoExchange.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(string userId);
        public string UserId { get; }
    }
}

using CryptoExchange.Application.Contracts.Identity;
using CryptoExchange.Application.Models.Identity;
using CryptoExchange.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

        public async Task<Customer> GetCustomer(string userId)
        {
            var customer = await _userManager.FindByIdAsync(userId);

            if (customer == null)
            {
                return null;
            }

            return new Customer
            {
                Email = customer.Email,
                Id = customer.Id,
                Firstname = customer.FirstName,
                Lastname = customer.LastName
            };
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            return customers.Select(q => new Customer
            {
                Id = q.Id,
                Email = q.Email,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }
    }
}

﻿using CryptoExchange.Application.Models.Identity;

namespace CryptoExchange.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
        public string UserId { get; }
    }
}

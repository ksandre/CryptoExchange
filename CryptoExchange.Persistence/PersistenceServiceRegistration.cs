using CryptoExchange.Application.Contracts.Persistence;
using CryptoExchange.Persistence.DatabaseContext;
using CryptoExchange.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("CryptoExchangeDbConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IExchangeRequestRepository, ExchangeRequestRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}

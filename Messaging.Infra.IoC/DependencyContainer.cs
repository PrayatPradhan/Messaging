using Messaging.Banking.Application.Interfaces;
using Messaging.Banking.Application.Services;
using Messaging.Banking.Data.Context;
using Messaging.Banking.Data.Repository;
using Messaging.Banking.Domain.Interfaces;
using Messaging.Domain.Core.Bus;
using Messaging.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Infra.IoC;
public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Domain Bus
        services.AddTransient<IEventBus, RabbitMqBus>();

        // Application Service
        services.AddTransient<IAccountService, AccountService>();

        //Data
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddScoped<BankingDbContext>();
    }
}

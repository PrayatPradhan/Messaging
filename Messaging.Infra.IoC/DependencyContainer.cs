using MediatR;
using Messagin.Transfer.Application.Interfaces;
using Messagin.Transfer.Application.Services;
using Messaging.Banking.Application.Interfaces;
using Messaging.Banking.Application.Services;
using Messaging.Banking.Data.Context;
using Messaging.Banking.Data.Repository;
using Messaging.Banking.Domain.CommandHandlers;
using Messaging.Banking.Domain.Commands;
using Messaging.Banking.Domain.Interfaces;
using Messaging.Domain.Core.Bus;
using Messaging.Infra.Bus;
using Messaging.Transfer.Data.Context;
using Messaging.Transfer.Data.Repository;
using Messaging.Transfer.Domain.EventHandlers;
using Messaging.Transfer.Domain.Events;
using Messaging.Transfer.Domain.Interfaces;
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
        services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
        {
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            return new RabbitMqBus(sp.GetService<IMediator>(), scopeFactory);
        });

        // Subscriptions
        services.AddTransient<TransferEventHandler>();

        // Domain Banking
        services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

        //events
        services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

        // Application Service
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ITransferService, TransferService>();

        //Data
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ITransferRepository, TransferRepository>();
        services.AddScoped<BankingDbContext>();
        services.AddScoped<TransferDbContext>();
    }
}

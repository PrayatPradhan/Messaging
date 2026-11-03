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
        services.AddTransient<IEventBus, RabbitMqBus>();
    }
}

using MediatR;
using Messaging.Domain.Core.Bus;
using Messaging.Domain.Core.Commands;
using Messaging.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Infra.Bus;

public sealed class RabbitMqBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;
    private readonly IServiceScopeFactory _scopefactory;

    public RabbitMqBus(IMediator mediator, IServiceScopeFactory scopefactory)
    {
        _mediator = mediator;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _scopefactory = scopefactory;
    }

    public async void Publish<T>(T @event) where T : Event
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        var eventName = @event.GetType().Name;
        await channel.QueueDeclareAsync(eventName, false, false, false, null);
        var message = JsonConvert.SerializeObject(@event);

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: "", routingKey: eventName, body: body);

    }

    public Task SendCommand<T>(T command) where T : Command
    {
        return _mediator.Send(command);
    }

    public void Subscribe<T, THandler>()
        where T : Event
        where THandler : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(THandler);

        if (!_eventTypes.Contains(typeof(T)))
            _eventTypes.Add(typeof(T));

        if (!_handlers.ContainsKey(eventName))
            _handlers.Add(eventName, new List<Type>());

        if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            throw new ArgumentException($"Handler type  {handlerType.Name} already registered for {eventName}");

        _handlers[eventName].Add(handlerType);

        StartBasicConsume<T>();
    }

    private async Task StartBasicConsume<T>() where T : Event
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        var eventName = typeof(T).Name;
        await channel.QueueDeclareAsync(eventName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += Consumer_Received;

        await channel.BasicConsumeAsync(eventName, true, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        try
        {
            await ProcessEvent(eventName, message);
        }
        catch (Exception ex) 
        {
            
        }           
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.ContainsKey(eventName))
        {
            using (var scope = _scopefactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);
                    if (handler == null) continue;

                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType!);

                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType!);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
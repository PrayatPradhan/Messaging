using MediatR;
using Messaging.Banking.Domain.Commands;
using Messaging.Banking.Domain.Events;
using Messaging.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Banking.Domain.CommandHandlers;

public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
{
    private readonly IEventBus _eventBus;

    public TransferCommandHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new TransferCreatedEvent(request.To, request.From, request.Amount));
        return Task.FromResult(true);
    }
}

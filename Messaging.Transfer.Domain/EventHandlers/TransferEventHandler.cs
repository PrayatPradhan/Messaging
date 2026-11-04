using Messaging.Domain.Core.Bus;
using Messaging.Transfer.Domain.Events;
using Messaging.Transfer.Domain.Interfaces;
using Messaging.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Transfer.Domain.EventHandlers;

public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
{
    private readonly ITransferRepository _transferRespository;
    public TransferEventHandler(ITransferRepository transferRespository)
    {
        _transferRespository = transferRespository;
    }
    public Task Handle(TransferCreatedEvent @event)
    {
        _transferRespository.Add(new TransferLog()
        {
            FromAccount = @event.From,
            ToAccount = @event.To,
            Amount = @event.Amount
        });

        return Task.CompletedTask;
    }
}

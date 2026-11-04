using Messagin.Transfer.Application.Interfaces;
using Messaging.Domain.Core.Bus;
using Messaging.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messagin.Transfer.Application.Services;

public class TransferService : ITransferService
{
    private readonly IEventBus _bus;
    private readonly ITransferRepository _repository;

    public TransferService(IEventBus bus, ITransferRepository repository)
    {
        _bus = bus;
        _repository = repository;
    }

    public IEnumerable<TransferLog> GetTransferLogs()
    {
        _repository.GetTransferLogs();
    }
}
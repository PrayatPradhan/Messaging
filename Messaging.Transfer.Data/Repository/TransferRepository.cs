using Messaging.Transfer.Data.Context;
using Messaging.Transfer.Domain.Interfaces;
using Messaging.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Transfer.Data.Repository;

public class TransferRepository : ITransferRepository
{
    private readonly TransferDbContext _dbContext;

    public TransferRepository(TransferDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<TransferLog> GetTransferLogs()
    {
        return _dbContext.TransferLogs;
    }

    public void Add(TransferLog transferLog)
    {
        _dbContext.TransferLogs.Add(transferLog);
        _dbContext.SaveChanges();
    }
}

using Messaging.Banking.Data.Context;
using Messaging.Banking.Domain.Interfaces;
using Messaging.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Banking.Data.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly BankingDbContext _dbContext;

    public AccountRepository(BankingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _dbContext.Accounts;
    }
}


using Messaging.Banking.Application.Interfaces;
using Messaging.Banking.Application.Models;
using Messaging.Banking.Domain.Commands;
using Messaging.Banking.Domain.Interfaces;
using Messaging.Banking.Domain.Models;
using Messaging.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Banking.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEventBus _bus;

    public AccountService(IAccountRepository accountRepository, IEventBus bus)
    {
        _accountRepository = accountRepository;
        _bus = bus;
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _accountRepository.GetAccounts();
    }

    public void Transfer(AccountTransfer accountTransfer)
    {
        var createTransferCommand = new CreateTransferCommand(
            accountTransfer.FromAccount,
            accountTransfer.ToAccount,
            accountTransfer.TransferAmount);

        _bus.SendCommand(createTransferCommand);
    }
}

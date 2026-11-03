using Messaging.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Banking.Application.Interfaces;

public interface IAccountService
{
    IEnumerable<Account> GetAccounts();
}


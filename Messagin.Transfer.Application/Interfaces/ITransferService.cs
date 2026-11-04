using Messaging.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messagin.Transfer.Application.Interfaces;
public interface ITransferService
{
    IEnumerable<TransferLog> GetTransferLogs();
}

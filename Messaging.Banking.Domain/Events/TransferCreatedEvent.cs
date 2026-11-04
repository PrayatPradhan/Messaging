using Messaging.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Banking.Domain.Events;
public class TransferCreatedEvent : Event
{
    public int From { get; protected set; }
    public int To { get; protected set; }
    public decimal Amount { get; protected set; }

    public TransferCreatedEvent(int to , int from, decimal amount)
    {
        To = to;
        From = from;

    }
}

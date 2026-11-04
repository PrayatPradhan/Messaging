using Messaging.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Transfer.Data.Context;

public class TransferDbContext : DbContext
{
    public TransferDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TransferLog> TransferLogs { get; set; }
}
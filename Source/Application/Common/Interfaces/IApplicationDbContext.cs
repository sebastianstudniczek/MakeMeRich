using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<FinancialAccount> FinancialAccounts { get; set; }
        DbSet<ExternalTransaction> ExternalTransactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

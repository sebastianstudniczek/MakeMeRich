using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<ExternalTransaction> ExternalTransactions { get; set; }
        public DbSet<InternalTransaction> InternalTransactions { get; set; }
        public DbSet<FinancialCategory> FinancialCategories { get ; set ; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}

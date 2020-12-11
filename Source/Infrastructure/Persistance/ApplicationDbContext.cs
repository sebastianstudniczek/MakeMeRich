using System.Reflection;
using System.Threading.Tasks;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<ExternalTransaction> ExternalTransactions { get; set; }
        public DbSet<InternalTransaction> InternalTransactions { get; set; }
        public DbSet<FinancialCategory> FinancialCategories { get ; set ; }
        public DbSet<ExternalTransactionCategory> ExternalTransactionCategories { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}

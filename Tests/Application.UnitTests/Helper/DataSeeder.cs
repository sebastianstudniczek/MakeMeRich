using System.Collections.Generic;

using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Helper
{
    internal static class DataSeeder
    {
        internal static void SeedSampleData(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            var financialAccounts = new List<FinancialAccount>
            {
                new FinancialAccount {Id = 1, Title = "BNP", CurrentBalance = 100},
                new FinancialAccount {Id = 2, Title = "Getin", CurrentBalance = 200},
                new FinancialAccount {Id = 3, Title = "PKO", CurrentBalance = 300}
            };

            context.FinancialAccounts.AddRange(financialAccounts);
            context.SaveChanges();
        }
    }
}

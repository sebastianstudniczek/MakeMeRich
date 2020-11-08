using System;
using System.Linq;
using System.Threading.Tasks;

using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;

using Microsoft.EntityFrameworkCore.Internal;

namespace MakeMeRich.Infrastructure.Persistance
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedSampleData(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.FinancialAccounts.Any())
            {
                context.FinancialAccounts.Add(new FinancialAccount
                {
                    Title = "BNP Paribas ROR",
                    CurrentBalance = 300.45,
                    AccountType = FinancialAccountType.Banking,
                    Transactions =
                    {
                        new ExternalTransaction
                        {
                            TransactionSideName = "Biedronka",
                            Type = ExternalTransactionType.Expense,
                            TotalAmount = 152.67,
                            DueDate = new DateTime(2018, 10, 1),
                            Description = "For breakfast",
                        },
                        new ExternalTransaction
                        {
                            TransactionSideName = "Lidl",
                            Type = ExternalTransactionType.Expense,
                            TotalAmount = 152.67,
                            DueDate = new DateTime(2018, 10, 1),
                            Description = "For dinner",
                        },
                        new InternalTransaction
                        {
                            TotalAmount = 152.67,
                            DueDate = new DateTime(2018, 10, 1),
                            Description = "For breakfast",
                            ReceivingAccountId = 2,
                        }
                    }
                });
            }

            await context.SaveChangesAsync();
        }
    }
}

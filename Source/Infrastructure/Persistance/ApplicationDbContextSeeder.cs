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
                    ExternalTransactions =
                    {
                        new ExternalTransaction
                        {
                            TransactionSideName = "Biedronka",
                            TransactionType = ExternalTransactionType.Expense,
                            TotalAmount = 152.67,
                            DueDate = new DateTime(2018, 10, 1),
                            Description = "For breakfast",
                        },
                        new ExternalTransaction
                        {
                            TransactionSideName = "Lidl",
                            TransactionType = ExternalTransactionType.Expense,
                            TotalAmount = 152.67,
                            DueDate = new DateTime(2018, 10, 1),
                            Description = "For dinner",
                        }
                    },
                    InternalTransactions =
                    {
                        new InternalTransaction
                        {
                            TotalAmount = 152.67,
                            DueDate = new DateTime(2018, 10, 1),
                            Description = "For breakfast",
                            ReceivingAccountId = 2,
                        },
                        new InternalTransaction
                        {
                            TotalAmount = 360.67,
                            DueDate = new DateTime(2019, 10, 1),
                            Description = "For lunch",
                            ReceivingAccountId = 1,
                        }
                    }
                });
            }

            await context.SaveChangesAsync();
        }
    }
}

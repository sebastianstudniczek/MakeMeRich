using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Infrastructure.Persistance
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.FinancialAccounts.Any())
            {

                var financialAccounts = new List<FinancialAccount>
                {
                    new FinancialAccount
                    {
                        Title = "BNP Paribas ROR",
                        CurrentBalance = 300.45,
                        AccountType = FinancialAccountType.Banking,
                    },
                    new FinancialAccount
                    {
                        Title = "Andrew's wallet",
                        CurrentBalance = 600.75,
                        AccountType = FinancialAccountType.Cash,
                    },
                };

                var externalTransactions = new List<ExternalTransaction>
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
                };

                var receivedInternalTransactions = new List<InternalTransaction>
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
                };

                var sendedInternalTransactions = new List<InternalTransaction>
                {
                    new InternalTransaction
                    {
                        TotalAmount = 52.67,
                        DueDate = new DateTime(2020, 1, 1),
                        Description = "For lunch",
                    },
                    new InternalTransaction
                    {
                        TotalAmount = 60.67,
                        DueDate = new DateTime(2020, 10, 1),
                        Description = "For dinner",
                    }
                };

                var financialCategories = new List<FinancialCategory>
                {
                    new FinancialCategory {Name = "Food Home"},
                    new FinancialCategory {Name = "Food Out"},
                };

                for (int i = 0; i < externalTransactions.Count; i++)
                {
                    financialAccounts[0].ExternalTransactions.Add(externalTransactions[i]);
                }

                for (int i = 0; i < receivedInternalTransactions.Count; i++)
                {
                    financialAccounts[0].ReceivedInternalTransactions.Add(receivedInternalTransactions[i]);
                }

                for (int i = 0; i < sendedInternalTransactions.Count; i++)
                {
                    financialAccounts[0].SendedInternalTransactions.Add(sendedInternalTransactions[i]);
                }

                var externalTransactionCategories = new List<ExternalTransactionCategory>
                {
                    new ExternalTransactionCategory
                    {
                        Amount = 0,
                        Description = "Some external category description",
                        ExternalTransaction = externalTransactions[0],
                        FinancialCategory = financialCategories[0]
                    },
                    new ExternalTransactionCategory
                    {
                        Amount = 0,
                        Description = "Some other external category description",
                        ExternalTransaction = externalTransactions[0],
                        FinancialCategory = financialCategories[1]
                    }
                };

                context.FinancialAccounts.AddRange(financialAccounts);
                context.FinancialCategories.AddRange(financialCategories);
                context.ExternalTransactions.AddRange(externalTransactions);
                context.InternalTransactions.AddRange(receivedInternalTransactions);
                context.InternalTransactions.AddRange(sendedInternalTransactions);
                context.ExternalTransactionCategories.AddRange(externalTransactionCategories);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}

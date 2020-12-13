using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace MakeMeRich.Infrastructure.Persistance
{
    public static class ApplicationDbContextSeeder
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "admin@localhost", Email = "admin@localhost" };

            if (userManager.Users.All(user => user.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!")
                    .ConfigureAwait(false);
            }
        }
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.FinancialAccounts.Any())
            {
                var financialCategories = new List<FinancialCategory>
                {
                    new FinancialCategory {Name = "Food Home"},
                    new FinancialCategory {Name = "Food Out"},
                };

                var financialAccounts = new List<FinancialAccount>
                {
                    new FinancialAccount
                    {
                        Title = "Andrew's wallet",
                        CurrentBalance = 600.75,
                        AccountType = FinancialAccountType.Cash,
                    },
                    new FinancialAccount
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
                                TransactionCategories =
                                {
                                    new ExternalTransactionCategory
                                    {
                                        Amount = 53,
                                        Description = "Some external category description",
                                        FinancialCategory = financialCategories[0]
                                    },
                                    new ExternalTransactionCategory
                                    {
                                        Amount = 451,
                                        Description = "Some other external category description",
                                        FinancialCategory = financialCategories[1]
                                    }
                                }
                            },
                            new ExternalTransaction
                            {
                                TransactionSideName = "Lidl",
                                TransactionType = ExternalTransactionType.Expense,
                                TotalAmount = 152.67,
                                DueDate = new DateTime(2018, 10, 1),
                                Description = "For dinner",
                            }
                        }
                    }
                };

                var receivedInternalTransactions = new List<InternalTransaction>
                {
                    new InternalTransaction
                    {
                        TotalAmount = 152.67,
                        DueDate = new DateTime(2018, 10, 1),
                        Description = "For breakfast",
                        ReceivingAccount = financialAccounts[1],
                        SendingAccount = financialAccounts[0],
                    },
                    new InternalTransaction
                    {
                        TotalAmount = 360.67,
                        DueDate = new DateTime(2019, 10, 1),
                        Description = "For lunch",
                        ReceivingAccount = financialAccounts[1],
                        SendingAccount = financialAccounts[0],
                    }
                };

                var sendedInternalTransactions = new List<InternalTransaction>
                {
                    new InternalTransaction
                    {
                        TotalAmount = 52.67,
                        DueDate = new DateTime(2020, 1, 1),
                        Description = "For lunch",
                        ReceivingAccount = financialAccounts[0],
                        SendingAccount = financialAccounts[1],
                    },
                    new InternalTransaction
                    {
                        TotalAmount = 60.67,
                        DueDate = new DateTime(2020, 10, 1),
                        Description = "For dinner",
                        ReceivingAccount = financialAccounts[0],
                        SendingAccount = financialAccounts[1],
                    }
                };

                context.FinancialCategories.AddRange(financialCategories);
                context.FinancialAccounts.AddRange(financialAccounts);
                context.InternalTransactions.AddRange(receivedInternalTransactions);
                context.InternalTransactions.AddRange(sendedInternalTransactions);

                await context
                    .SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}

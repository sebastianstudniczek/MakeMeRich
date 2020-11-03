using System;
using System.Collections.Generic;

using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
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

            var financialAccounts = GetSampleFinancialAccounts();
            var externalTransactions = GetSampleExternalTransactions();
            var internalTransactions = GetSampleInternalTransactions();
            var financialCategories = GetSampleFinancialCategories();

            context.FinancialAccounts.AddRange(financialAccounts);
            context.ExternalTransactions.AddRange(externalTransactions);
            context.InternalTransactions.AddRange(internalTransactions);
            context.FinancialCategories.AddRange(financialCategories);

            context.SaveChanges();
        }

        private static List<FinancialAccount> GetSampleFinancialAccounts()
        {
            var financialAccounts =  new List<FinancialAccount>
            {
                new FinancialAccount { Id = 1, Title = "BNP", CurrentBalance = 100},
                new FinancialAccount { Id = 2, Title = "Getin", CurrentBalance = 200},
                new FinancialAccount { Id = 3, Title = "PKO", CurrentBalance = 300}
            };

            return financialAccounts;
        }

        private static List<ExternalTransaction> GetSampleExternalTransactions()
        {
            var externalTransactions = new List<ExternalTransaction>
            {
                new ExternalTransaction
                {
                    Id = 1,
                    TransactionSideName = "Biedronka",
                    TotalAmount = 250,
                    Type = ExternalFinancialTransactionType.Expense,
                    DueDate = new DateTime(2019, 11, 12),
                    Description = "First external transaction",

                    FinancialAccountId = 1,
                },
                new ExternalTransaction
                {
                    Id = 2,
                    TransactionSideName = "Lidl",
                    TotalAmount = 300.67,
                    Type = ExternalFinancialTransactionType.Income,
                    DueDate = new DateTime(2011, 11, 12),
                    Description = "Second external transaction",

                    FinancialAccountId = 1,
                },
                new ExternalTransaction
                {
                    Id = 3,
                    TransactionSideName = "Żabka",
                    TotalAmount = 150.67,
                    Type = ExternalFinancialTransactionType.Expense,
                    DueDate = new DateTime(2013, 11, 12),
                    Description = "Third external transaction",

                    FinancialAccountId = 2,
                },
            };

            return externalTransactions;
        }

        private static List<InternalTransaction> GetSampleInternalTransactions()
        {
            var internalTransactions = new List<InternalTransaction>
            {
                new InternalTransaction
                {
                    Id = 1,
                    TotalAmount = 335.87,
                    DueDate = new DateTime(2011, 5, 12),
                    Description = "First internal transaction",

                    FinancialAccountId = 1,
                    ReceivingAccountId = 2
                },

                new InternalTransaction
                {
                    Id = 2,
                    TotalAmount = 115.87,
                    DueDate = new DateTime(2016, 5, 12),
                    Description = "First internal transaction",

                    FinancialAccountId = 2,
                    ReceivingAccountId = 1
                },

                new InternalTransaction
                {
                    Id = 2,
                    TotalAmount = 115.87,
                    DueDate = new DateTime(2016, 5, 12),
                    Description = "First internal transaction",

                    FinancialAccountId = 3,
                    ReceivingAccountId = 2
                },
            };

            return internalTransactions;
        }

        private static List<FinancialCategory> GetSampleFinancialCategories()
        {
            var financialCategories = new List<FinancialCategory>
            {
                new FinancialCategory { Id = 1, Name = "House" },
                new FinancialCategory { Id = 2, Name = "Medics" },
                new FinancialCategory { Id = 3, Name = "Food"}
            };

            return financialCategories;
        }
    }
}

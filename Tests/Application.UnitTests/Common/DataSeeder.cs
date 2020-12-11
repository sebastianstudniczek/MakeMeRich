using System;
using System.Collections.Generic;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Common
{
    internal static class DataSeeder
    {
        internal static int FinancialAccountsCount { get; private set; }
        internal static int FinancialCategoriesCount { get; private set; }
        internal static void GetSampleFinancialAccounts(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var financialAccounts = new List<FinancialAccount>
            {
                new FinancialAccount { Id = 1, Title = "BNP", CurrentBalance = 100},
                new FinancialAccount { Id = 2, Title = "Getin", CurrentBalance = 200},
                new FinancialAccount { Id = 3, Title = "PKO", CurrentBalance = 300}
            };

            FinancialAccountsCount = financialAccounts.Count;
            context.FinancialAccounts.AddRange(financialAccounts);
            context.SaveChanges();
        }

        internal static void GetSampleExternalTransactions(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var externalTransactions = new List<ExternalTransaction>
            {
                new ExternalTransaction
                {
                    Id = 1,
                    TransactionSideName = "Biedronka",
                    TotalAmount = 250,
                    TransactionType = ExternalTransactionType.Expense,
                    DueDate = new DateTime(2019, 11, 12),
                    Description = "First external transaction",

                    FinancialAccountId = 1,
                },
                new ExternalTransaction
                {
                    Id = 2,
                    TransactionSideName = "Lidl",
                    TotalAmount = 300.67,
                    TransactionType = ExternalTransactionType.Income,
                    DueDate = new DateTime(2011, 11, 12),
                    Description = "Second external transaction",

                    FinancialAccountId = 1,
                },
                new ExternalTransaction
                {
                    Id = 3,
                    TransactionSideName = "Żabka",
                    TotalAmount = 150.67,
                    TransactionType = ExternalTransactionType.Expense,
                    DueDate = new DateTime(2013, 11, 12),
                    Description = "Third external transaction",

                    FinancialAccountId = 2,
                },
                new ExternalTransaction
                {
                    Id = 4,
                    TransactionSideName = "Żabka",
                    TotalAmount = 1600.67,
                    TransactionType = ExternalTransactionType.Expense,
                    DueDate = new DateTime(2013, 11, 12),
                    Description = "Fourth external transaction",

                    FinancialAccountId = 2,
                },
            };

            context.ExternalTransactions.AddRange(externalTransactions);
            context.SaveChanges();
        }

        internal static void GetSampleInternalTransactions(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var internalTransactions = new List<InternalTransaction>
            {
                new InternalTransaction
                {
                    Id = 1,
                    TotalAmount = 335.87,
                    DueDate = new DateTime(2011, 5, 12),
                    Description = "First internal transaction",

                    SendingAccountId = 1,
                    ReceivingAccountId = 2
                },

                new InternalTransaction
                {
                    Id = 2,
                    TotalAmount = 115.87,
                    DueDate = new DateTime(2016, 5, 12),
                    Description = "Second internal transaction",

                    SendingAccountId = 2,
                    ReceivingAccountId = 1
                },

                new InternalTransaction
                {
                    Id = 3,
                    TotalAmount = 85.87,
                    DueDate = new DateTime(2014, 5, 12),
                    Description = "Third internal transaction",

                    SendingAccountId = 3,
                    ReceivingAccountId = 2
                },
            };

            context.InternalTransactions.AddRange(internalTransactions);
            context.SaveChanges();
        }

        internal static void GetSampleFinancialCategories(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var financialCategories = new List<FinancialCategory>
            {
                new FinancialCategory { Id = 1, Name = "House" },
                new FinancialCategory { Id = 2, Name = "Medics" },
                new FinancialCategory { Id = 3, Name = "Food"}
            };

            FinancialCategoriesCount = financialCategories.Count;
            context.FinancialCategories.AddRange(financialCategories);
            context.SaveChanges();
        }
    }
}

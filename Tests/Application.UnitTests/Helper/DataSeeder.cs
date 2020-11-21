﻿using System;
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
        internal static void GetSampleFinancialAccounts(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var financialAccounts =  new List<FinancialAccount>
            {
                new FinancialAccount { Id = 1, Title = "BNP", CurrentBalance = 100},
                new FinancialAccount { Id = 2, Title = "Getin", CurrentBalance = 200},
                new FinancialAccount { Id = 3, Title = "PKO", CurrentBalance = 300}
            };

            context.FinancialAccounts.AddRange(financialAccounts);
            context.ExternalTransactions.AddRange(externalTransactions);
            context.InternalTransactions.AddRange(internalTransactions);
            context.FinancialCategories.AddRange(financialCategories);

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

                    FinancialAccountId = 1,
                    ReceivingAccountId = 2
                },

                new InternalTransaction
                {
                    Id = 2,
                    TotalAmount = 115.87,
                    DueDate = new DateTime(2016, 5, 12),
                    Description = "Second internal transaction",

                    FinancialAccountId = 2,
                    ReceivingAccountId = 1
                },

                new InternalTransaction
                {
                    Id = 3,
                    TotalAmount = 85.87,
                    DueDate = new DateTime(2014, 5, 12),
                    Description = "Third internal transaction",

                    FinancialAccountId = 3,
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

            context.FinancialCategories.AddRange(financialCategories);
            context.SaveChanges();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class UpdateExternalTransactionCommandHandlerTests
    {
        public void ShouldRequireValidExternalTransactionId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldRequireValidExternalTransactionId")
                .Options;

        }

        public async Task ShouldUpdateExternalTransaction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldUpdateExternalTransaction")
                .Options;

            var entity = new ExternalTransaction
            {
                Id = 2,
                TransactionSideName = "Biedronka",
                TotalAmount = 250,
                DueDate = new DateTime(2018, 4, 12),
                Description = "Base Shopping",
                Type = ExternalFinancialTransactionType.Expense,
                FinancialAccountId = 1,
                FinancialAccount = new FinancialAccount
                {
                    Id = 1,
                    Title = "mBank",
                    CurrentBalance = 250,
                    Type = FinancialAccountType.Banking
                }
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(entity);
                context.SaveChanges();
            }

            var command = new UpdateExternalTransactionCommand
            {
                Id = entity.Id,
                TransactionSideName = "Kaufland",
                TotalAmount = 350,
                DueDate = new DateTime(2016, 4, 12),
                Description = "Shopping",
                Type = ExternalFinancialTransactionType.Income,
                FinancialAccountId = 1,
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new UpdateExternalTransactionCommandHandler(context);

                await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(options))
            {
                var externalTransaction = await context.FindAsync<ExternalTransaction>(command.Id);

                externalTransaction.TransactionSideName.Should().Be(command.TransactionSideName);
                externalTransaction.TotalAmount.Should().Be(command.TotalAmount);
                externalTransaction.DueDate.Should().Be(command.DueDate);
                externalTransaction.Description.Should().Be(command.Description);
                externalTransaction.Type.Should().Be(command.Type);
                externalTransaction.FinancialAccountId.Should().Be(command.FinancialAccountId);
            }
        }
    }
}

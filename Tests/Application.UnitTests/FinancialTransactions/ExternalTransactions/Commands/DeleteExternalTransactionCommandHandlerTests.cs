using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class DeleteExternalTransactionCommandHandlerTests
    {
        [Fact]
        public async Task ShouldDeleteExternalTransaction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldDeleteFinancialAccount")
                .Options;

            var entity = new ExternalTransaction
            {
                Id = 2,
                TransactionSideName = "Allegro",
                TotalAmount = 526,
                DueDate = new DateTime(2019, 10, 26),
                Description = "Sample shopping",
                Type = ExternalFinancialTransactionType.Expense
                // TODO :Categories
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.ExternalTransactions.Add(entity);
                context.SaveChanges();
            }

            var command = new DeleteExternalTransactionCommand
            {
                Id = entity.Id
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new DeleteExternalTransactionCommandHandler(context);

                await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(options))
            {
                var externalTransaction = await context.FindAsync<ExternalTransaction>(entity.Id);

                externalTransaction.Should().BeNull();
            }
        }
    }
}

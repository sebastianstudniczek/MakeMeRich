using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class DeleteExternalTransactionCommandHandlerTests : CommandHandlerTestBase
    {
        [Fact]
        public async Task ShouldDeleteExternalTransaction()
        {
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

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                context.ExternalTransactions.Add(entity);
                context.SaveChanges();
            }

            var command = new DeleteExternalTransactionCommand
            {
                Id = entity.Id
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler =
                    new DeleteExternalTransactionCommandHandler(context);

                await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var externalTransaction = await context.FindAsync<ExternalTransaction>(entity.Id);

                externalTransaction.Should().BeNull();
            }
        }
    }
}

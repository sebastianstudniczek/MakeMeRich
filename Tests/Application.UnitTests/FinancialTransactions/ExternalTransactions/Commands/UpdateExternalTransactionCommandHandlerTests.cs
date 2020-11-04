using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class UpdateExternalTransactionCommandHandlerTests : CommandHandlerTestBase
    {
        [Fact]
        public void ShouldRequireValidExternalTransactionId()
        {
            var command = new UpdateExternalTransactionCommand
            {
                Id = 99,
                TransactionSideName = "Lidl"
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateExternalTransactionCommandHandler(context);

                FluentActions.Invoking(() => commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldUpdateExternalTransaction()
        {
            DataSeeder.SeedSampleData(DbContextOptions);
            var command = new UpdateExternalTransactionCommand
            {
                Id = 2,
                TransactionSideName = "Kaufland",
                TotalAmount = 350,
                DueDate = new DateTime(2016, 4, 12),
                Description = "Shopping",
                Type = ExternalFinancialTransactionType.Income,
                FinancialAccountId = 1,
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateExternalTransactionCommandHandler(context);
                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
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

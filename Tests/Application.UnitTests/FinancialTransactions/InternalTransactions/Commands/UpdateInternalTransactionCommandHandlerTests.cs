using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.UpdateInternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Commands
{
    public class UpdateInternalTransactionCommandHandlerTests : HandlerTest
    {
        [Fact]
        public void ShouldRequireValidInternalTransactionId()
        {
            var command = new UpdateInternalTransactionCommand
            {
                Id = 99,
                TotalAmount = 100,
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateInternalTransactionCommandHandler(context);

                FluentActions.Invoking(() => commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldUpdateInternalTransaction()
        {
            DataSeeder.GetSampleInternalTransactions(DbContextOptions);
            var command = new UpdateInternalTransactionCommand
            {
                Id = 2,
                TotalAmount = 300,
                DueDate = new DateTime(2018, 4, 3),
                Description = "Updated internal transaction",
                SendingAccountId = 5,
                ReceivingAccountId = 2
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateInternalTransactionCommandHandler(context);
                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var internalTransaction = await context.FindAsync<InternalTransaction>(command.Id);

                internalTransaction.TotalAmount.Should().Be(command.TotalAmount);
                internalTransaction.DueDate.Should().Be(command.DueDate);
                internalTransaction.Description.Should().Be(command.Description);
                internalTransaction.SendingAccountId.Should().Be(5);
                internalTransaction.ReceivingAccountId.Should().Be(2);
            }
        }
    }
}

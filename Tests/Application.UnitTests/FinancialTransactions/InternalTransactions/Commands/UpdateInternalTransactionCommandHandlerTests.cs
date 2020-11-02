using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.UpdateInternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Commands
{
    public class UpdateInternalTransactionCommandHandlerTests : CommandHandlerTestBase
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

                FluentActions.Invoking(() =>
                    commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldUpdateInternalTransaction()
        {
            var entity = new InternalTransaction
            {
                TotalAmount = 100,
                DueDate = new DateTime(2017, 3, 3),
                Description = "Base internal transaction",
                FinancialAccountId = 2,
                ReceivingAccountId = 5
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                context.Add(entity);
                context.SaveChanges();
            }

            var command = new UpdateInternalTransactionCommand
            {
                Id = entity.Id,
                TotalAmount = 300,
                DueDate = new DateTime(2018, 4, 3),
                Description = "Updated internal transaction",
                FinancialAccountId = 5,
                ReceivingAccountId = 2
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateInternalTransactionCommandHandler(context);

                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var internalTransaction = await context.FindAsync<InternalTransaction>(entity.Id);

                internalTransaction.TotalAmount.Should().Be(command.TotalAmount);
                internalTransaction.DueDate.Should().Be(command.DueDate);
                internalTransaction.Description.Should().Be(command.Description);
                internalTransaction.FinancialAccountId.Should().Be(5);
                internalTransaction.ReceivingAccountId.Should().Be(2);
            }
        }
    }
}

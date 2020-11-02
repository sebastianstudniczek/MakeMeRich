using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Commands
{
    public class UpdateInternalTransactionCommandHandlerTests
    {
        [Fact]
        public async Task ShouldUpdateInternalTransaction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldUpdateInternalTransaction")
                .Options;

            var entity = new InternalTransaction
            {
                TotalAmount = 100,
                DueDate = new DateTime(2017, 3, 3),
                Description = "Base internal transaction",
                FinancialAccountId = 2,
                ReceivingAccountId = 5
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(entity);
                context.SaveChanges();
            }

            var command = new UpdateInternalTransactionCommand
            {
                TotalAmount = 300,
                DueDate = new DateTime(2018, 4, 3),
                Description = "Updated internal transaction",
                FinancialAccountId = 5,
                ReceivingAccountId = 2
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler = new UpdateInternalTransactionCommandHandler(context);

                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
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

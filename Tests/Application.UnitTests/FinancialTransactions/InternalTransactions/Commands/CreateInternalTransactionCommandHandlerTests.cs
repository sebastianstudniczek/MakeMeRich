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
    public class CreateInternalTransactionCommandHandlerTests
    {
        [Fact]
        public async Task ShouldCreateInternalTransaction()
        {
            int id;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldCreateInternalExpense")
                .Options;

            var command = new CreateInternalTransactionCommand
            {
                SendingAccountId = 1,
                ReceivingAccountId = 2,
                TotalAmount = 255,
                Description = "Some internal transaction",
                DueDate = new DateTime(2016, 7, 12)
                // TODO: Categories
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler = new CreateInternalTransactionCommandHandler(context);

                id = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var internalTransaction = context.FindAsync<InternalTransaction>(id);

                internalTransaction.Should().NotBeNull();

            }
        }
    }
}

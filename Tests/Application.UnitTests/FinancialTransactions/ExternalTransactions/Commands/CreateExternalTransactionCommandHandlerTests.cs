using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class CreateExternalTransactionCommandHandlerTests
    {
        [Fact]
        public async Task ShouldCreateExpense()
        {
            int id;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldCreateExpense))
                .Options;

            var command = new CreateExternalTransactionCommand
            {
                TransactionSideName = "Allegro",
                TotalAmount = 526,
                DueDate = new DateTime(2019, 10, 26),
                Description = "Sample shopping",
                Type = ExternalFinancialTransactionType.Expense
                // TODO: Categories
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler = new CreateExternalTransactionCommandHandler(context);

                id = await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(options))
            {
                var externalTransaction = await context.FindAsync<ExternalTransaction>(id);

                externalTransaction.Should().NotBeNull();
                externalTransaction.TransactionSideName.Should().Be(command.TransactionSideName);
                externalTransaction.TotalAmount.Should().Be(command.TotalAmount);
                externalTransaction.DueDate.Should().Be(command.DueDate);
                externalTransaction.Description.Should().Be(command.Description);
                externalTransaction.Type.Should().Be(command.Type);
            }
        }
    }
}

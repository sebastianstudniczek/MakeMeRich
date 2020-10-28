using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialTransactions.Expenses.Commands.CreateExpense;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.Expenses.Commands
{
    public class CreateExpenseCommandHandlerTests
    {
        [Fact]
        public async Task ShouldCreateExpense()
        {
            int id;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldCreateExpense")
                .Options;

            var command = new CreateExpenseCommand
            {
                PayeeName = "Allegro",
                TotalAmount = 526,
                DueDate = new DateTime(2019, 10, 26),
                Description = "Sample shopping",
                // TODO: Categories
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler = new CreateExpenseCommandHandler(context);

                id = await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(options))
            {
                var expense = await context.FindAsync<Expense>(id);

                expense.Should().NotBeNull();
                expense.PayeeName.Should().Be(command.PayeeName);
                expense.TotalAmount.Should().Be(command.TotalAmount);
                expense.DueDate.Should().Be(command.DueDate);
                expense.Description.Should().Be(command.Description);
            }
        }
    }
}

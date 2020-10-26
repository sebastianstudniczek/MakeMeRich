using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class CreateFinancialAccountCommandHandlerTests
    {
        [Fact]
        public async Task ShouldCreateFinancialAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateFinancialAccountCommandHandlerTest")
                .Options;

            var command = new CreateFinancialAccountCommand
            {
                Title = "BNP Private",
                CurrentBalance = 250
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new CreateFinancialAccountCommandHandler(context);

                int id = await commandHandler.Handle(command, new CancellationToken());
                var financialAccount = await context.FindAsync<FinancialAccount>(id);

                financialAccount.Should().NotBeNull();
                financialAccount.Title.Should().Be(command.Title);
                financialAccount.CurrentBalance.Should().Be(250);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.IntegrationTests.FinancialAccounts.Commands
{
    public class CreateFinancialAccountCommandHandlerTests
    {
        public CreateFinancialAccountCommandHandlerTests()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "createFinancialAccountCommandHandlerTest")
                .Options ;
        }

        public DbContextOptions<ApplicationDbContext> ContextOptions { get; set; }

        [Fact]
        public async Task ShouldCreateFinancialAccount()
        {
            var command = new CreateFinancialAccountCommand
            {
                Title = "BNP Private",
                CurrentBalance = 250
            };

            using (var context = new ApplicationDbContext(ContextOptions))
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

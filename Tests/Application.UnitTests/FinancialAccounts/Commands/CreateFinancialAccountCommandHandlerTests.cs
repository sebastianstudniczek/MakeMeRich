using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class CreateFinancialAccountCommandHandlerTests : CommandHandlerTestBase
    {
        [Fact]
        public async Task ShouldCreateFinancialAccount()
        {
            int id;
            var command = new CreateFinancialAccountCommand
            {
                Title = "BNP Private",
                CurrentBalance = 250,
                Type = FinancialAccountType.Banking
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new CreateFinancialAccountCommandHandler(context);
                id = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(id);

                financialAccount.Should().NotBeNull();
                financialAccount.Title.Should().Be(command.Title);
                financialAccount.CurrentBalance.Should().Be(command.CurrentBalance);
                financialAccount.Type.Should().Be(command.Type);
            }
        }
    }
}

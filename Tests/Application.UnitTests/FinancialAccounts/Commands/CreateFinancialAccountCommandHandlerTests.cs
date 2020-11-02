using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Enums;
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
            int id;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldCreateFinancialAccount))
                .Options;

            var command = new CreateFinancialAccountCommand
            {
                Title = "BNP Private",
                CurrentBalance = 250,
                Type = FinancialAccountType.Banking
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new CreateFinancialAccountCommandHandler(context);

                id = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
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

using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class UpdateFinancialAccountCommandHandlerTests : CommandHandlerTestBase
    {
        [Fact]
        public void ShouldRequireValidFinancialAccountId()
        {
            var command = new UpdateFinancialAccountCommand
            {
                Id = 99,
                Title = "Reifeizen"
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler =
                    new UpdateFinancialAccountCommandHandler(context);

                FluentActions.Invoking(() =>
                    commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldUpdateFinancialAccount()
        {
            var entity = new FinancialAccount
            {
                Id = 3,
                Title = "ING Bank",
                CurrentBalance = 500
            };

            using(var context = new ApplicationDbContext(DbContextOptions))
            {
                context.Add(entity);
                context.SaveChanges();
            }

            var updateCommand = new UpdateFinancialAccountCommand
            {
                Id = 3,
                Title = "ING Bank Śląski",
                CurrentBalance = 1500
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var command =
                    new UpdateFinancialAccountCommandHandler(context);

                await command.Handle(updateCommand, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(entity.Id);

                financialAccount.Title.Should().Be(updateCommand.Title);
                financialAccount.CurrentBalance.Should().Be(updateCommand.CurrentBalance);
            }
        }
    }
}

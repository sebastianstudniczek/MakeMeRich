using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class UpdateFinancialAccountCommandHandlerTests
    {
        [Fact]
        public void ShouldRequireValidFinancialAccountId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldRequireValidFinancialAccountId")
                .Options;

            var command = new UpdateFinancialAccountCommand
            {
                Id = 99,
                Title = "Reifeizen"
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new UpdateFinancialAccountCommandHandler(context);

                FluentActions.Invoking(() =>
                    commandHandler.Handle(command, new CancellationToken()))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldUpdateFinancialAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldUpdateFinancialAccount")
                .Options;

            var entity = new FinancialAccount
            {
                Id = 3,
                Title = "ING Bank",
                CurrentBalance = 500
            };

            using(var context = new ApplicationDbContext(options))
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

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new UpdateFinancialAccountCommandHandler(context);

                await commandHandler.Handle(updateCommand, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(options))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(entity.Id);

                financialAccount.Title.Should().Be(updateCommand.Title);
                financialAccount.CurrentBalance.Should().Be(updateCommand.CurrentBalance);
            }
        }
    }
}

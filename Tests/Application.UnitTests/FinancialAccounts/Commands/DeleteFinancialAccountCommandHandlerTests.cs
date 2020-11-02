using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class DeleteFinancialAccountCommandHandlerTests
    {
        [Fact]
        public async Task ShouldDeleteFinancialAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldDeleteFinancialAccount))
                .Options;

            DataSeeder.SeedSampleData(options);

            var command = new DeleteFinancialAccountCommand
            {
                Id = 1
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new DeleteFinancialAccountCommandHandler(context);

                await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(options))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(command.Id);

                financialAccount.Should().BeNull();
            }
        }
    }
}

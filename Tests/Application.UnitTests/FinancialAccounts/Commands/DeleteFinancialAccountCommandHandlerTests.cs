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
        public DeleteFinancialAccountCommandHandlerTests()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "deleteFinancialAccountCommandHandlerTest")
                .Options;

            DataSeeder.Seed(ContextOptions);
        }
        public DbContextOptions<ApplicationDbContext> ContextOptions { get; set; }

        [Fact]
        public async Task ShouldDeleteFinancialAccount()
        {
            var command = new DeleteFinancialAccountCommand
            {
                Id = 1
            };

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var commandHandler =
                    new DeleteFinancialAccountCommandHandler(context);

                await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(command.Id);

                financialAccount.Should().BeNull();
            }
        }
    }
}

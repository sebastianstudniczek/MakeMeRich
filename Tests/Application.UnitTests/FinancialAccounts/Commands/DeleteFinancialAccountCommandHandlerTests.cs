using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class DeleteFinancialAccountCommandHandlerTests : HandlerTest
    {
        [Fact]
        public void ShouldRequireValidFinancialAccountId()
        {
            var command = new DeleteFinancialAccountCommand { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteFinancialAccountCommandHandler(context);

                FluentActions.Invoking(() => commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldDeleteFinancialAccount()
        {
            DataSeeder.GetSampleFinancialAccounts(DbContextOptions);
            var command = new DeleteFinancialAccountCommand { Id = 1 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteFinancialAccountCommandHandler(context);
                await commandHandler.Handle(command, new CancellationToken());
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(command.Id);

                financialAccount.Should().BeNull();
            }
        }
    }
}

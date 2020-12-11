using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class DeleteExternalTransactionCommandHandlerTests : HandlerTest
    {
        [Fact]
        public void ShouldRequireValidExternalTransactionId()
        {
            var command = new DeleteExternalTransactionCommand { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteExternalTransactionCommandHandler(context);

                FluentActions.Invoking(() => commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldDeleteExternalTransaction()
        {
            DataSeeder.GetSampleExternalTransactions(DbContextOptions);
            var command = new DeleteExternalTransactionCommand { Id = 1 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteExternalTransactionCommandHandler(context);
                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var externalTransaction = await context.FindAsync<ExternalTransaction>(command.Id);

                externalTransaction.Should().BeNull();
            }
        }
    }
}

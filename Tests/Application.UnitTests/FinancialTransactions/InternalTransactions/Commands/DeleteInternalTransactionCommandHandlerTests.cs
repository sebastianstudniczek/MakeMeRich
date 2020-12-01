using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.DeleteInternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Commands
{
    public class DeleteInternalTransactionCommandHandlerTests : HandlerTest
    {
        [Fact]
        public void ShouldRequireValidInternalTransactionId()
        {
            var command = new DeleteInternalTransactionCommand { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteInternalTransactionCommandHandler(context);

                FluentActions.Invoking(() => commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldDeleteInternalTransaction()
        {
            DataSeeder.GetSampleInternalTransactions(DbContextOptions);
            var command = new DeleteInternalTransactionCommand{ Id = 2 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteInternalTransactionCommandHandler(context);
                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var internalTransaction = await context.FindAsync<InternalTransaction>(command.Id);

                internalTransaction.Should().BeNull();
            }
        }
    }
}

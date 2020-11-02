using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.DeleteInternalTransaction;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Commands
{
    public class DeleteInternalTransactionCommandHandlerTests
    {
        [Fact]
        public void ShouldRequireValidInternalTransactionId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldRequireValidInternalTransactionId))
                .Options;

            var command = new DeleteInternalTransactionCommand
            {
                Id = 99
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler = new DeleteInternalTransactionCommandHandler(context);

                FluentActions.Invoking(() =>
                    commandHandler.Handle(command, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldDeleteInternalTransaction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldDeleteInternalTransaction))
                .Options;

            var entity = new InternalTransaction
            {
                Id = 3,
                TotalAmount = 567
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.InternalTransactions.Add(entity);
                context.SaveChanges();
            }

            var command = new DeleteInternalTransactionCommand
            {
                Id = entity.Id
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new DeleteInternalTransactionCommandHandler(context);

                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var internalTransaction = await context.FindAsync<InternalTransaction>(entity.Id);

                internalTransaction.Should().BeNull();
            }
        }
    }
}

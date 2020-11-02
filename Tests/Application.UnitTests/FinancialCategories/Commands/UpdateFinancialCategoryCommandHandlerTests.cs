using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class UpdateFinancialCategoryCommandHandlerTests : CommandHandlerTestBase
    {
        [Fact]
        public async Task ShouldUpdateFinancialCategory()
        {
            var entity = new FinancialCategory
            {
                Name = "House"
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                context.Add(entity);
                context.SaveChanges();
            }

            var command = new UpdateFinancialCategoryCommand
            {
                Id = entity.Id,
                Name = "Gear"
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateFinancialCategoryCommandHandler(context);

                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialTransaction = await context.FindAsync<FinancialCategory>(entity.Id);

                financialTransaction.Name.Should().Be(command.Name);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class UpdateFinancialCategoryCommandHandlerTests
    {
        [Fact]
        public async Task ShouldUpdateFinancialCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldUpdateFinancialCategory))
                .Options;

            var entity = new FinancialCategory
            {
                Name = "House"
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Add(entity);
                context.SaveChanges();
            }

            var command = new UpdateFinancialCategoryCommand
            {
                Id = entity.Id,
                Name = "Gear"
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler = new UpdateFinancialCategoryCommandHandler(context);

                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var financialTransaction = await context.FindAsync<FinancialCategory>(entity.Id);

                financialTransaction.Name.Should().Be(command.Name);
            }
        }
    }
}

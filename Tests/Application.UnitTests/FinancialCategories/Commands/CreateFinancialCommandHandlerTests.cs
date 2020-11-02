using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategories;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class CreateFinancialCommandHandlerTests
    {
        [Fact]
        public async Task ShouldCreateFinancialCategory()
        {
            int id;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(ShouldCreateFinancialCategory))
                .Options;

            var command = new CreateFinancialCategoryCommand
            {
                Name = "House"
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new CreateFinancialCategoryCommandHandler(context);

                id = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var financialCategory = await context.FindAsync<FinancialCategory>(id);

                financialCategory.Should().NotBeNull();
                financialCategory.Name.Should().Be(command.Name);
            }
        }
    }
}

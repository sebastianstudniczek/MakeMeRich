using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class CreateFinancialCategoryCommandHandlerTests : HandlerTest
    {
        [Fact]
        public async Task ShouldCreateFinancialCategory()
        {
            int id;
            var command = new CreateFinancialCategoryCommand { Name = "House" };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new CreateFinancialCategoryCommandHandler(context);
                id = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialCategory = await context.FindAsync<FinancialCategory>(id);

                financialCategory.Should().NotBeNull();
                financialCategory.Name.Should().Be(command.Name);
            }
        }
    }
}

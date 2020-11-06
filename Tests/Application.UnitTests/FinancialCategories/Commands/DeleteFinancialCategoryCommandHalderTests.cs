using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialCategories.Commands.DeleteFinancialCategory;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class DeleteFinancialCategoryCommandHalderTests : CommandHandlerTestBase
    {
        [Fact]
        public async Task ShouldDeleteFinancialCategory()
        {
            DataSeeder.GetSampleFinancialCategories(DbContextOptions);
            var command = new DeleteFinancialCategoryCommand { Id = 2 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteFinancialCategoryCommandHandler(context);
                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialCategory = await context.FindAsync<FinancialCategory>(command.Id);

                financialCategory.Should().BeNull();
            }
        }
    }
}

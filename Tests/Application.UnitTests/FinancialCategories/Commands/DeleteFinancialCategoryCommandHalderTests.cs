using System.Threading;

using MakeMeRich.Application.FinancialCategories.Commands.DeleteFinancialCategory;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class DeleteFinancialCategoryCommandHalderTests : CommandHandlerTestBase
    {
        public void ShouldDeleteFinancialCategory()
        {
            DataSeeder.SeedSampleData(DbContextOptions);
            var command = new DeleteFinancialCategoryCommand
            {
                Id = 2
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new DeleteFinancialCategoryCommandHandler(context);
                commandHandler.Handler(context, CancellationToken.None);
            }
        }
    }
}

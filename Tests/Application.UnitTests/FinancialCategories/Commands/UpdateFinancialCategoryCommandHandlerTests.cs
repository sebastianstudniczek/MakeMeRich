using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class UpdateFinancialCategoryCommandHandlerTests : HandlerTest
    {
        [Fact]
        public async Task ShouldUpdateFinancialCategory()
        {
            DataSeeder.GetSampleFinancialCategories(DbContextOptions);
            var command = new UpdateFinancialCategoryCommand
            {
                Id = 1,
                Name = "Gear"
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new UpdateFinancialCategoryCommandHandler(context);
                await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialTransaction = await context.FindAsync<FinancialCategory>(command.Id);

                financialTransaction.Name.Should().Be(command.Name);
            }
        }
    }
}

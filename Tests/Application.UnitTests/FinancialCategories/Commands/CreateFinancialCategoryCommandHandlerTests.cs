using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Commands
{
    public class CreateFinancialCategoryCommandHandlerTests : HandlerTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public CreateFinancialCategoryCommandHandlerTests()
        {
            _configuration = new MapperConfiguration(
              config => config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task ShouldCreateFinancialCategory()
        {
            FinancialCategoryDto dto;
            var command = new CreateFinancialCategoryCommand { Name = "House" };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new CreateFinancialCategoryCommandHandler(context, _mapper);
                dto = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialCategory = await context.FindAsync<FinancialCategory>(dto.Id);

                financialCategory.Should().NotBeNull();
                financialCategory.Name.Should().Be(command.Name);
            }
        }
    }
}

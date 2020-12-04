using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Queries
{
    public class GetFinanialCategoriesQueryHandlerTests : HandlerTest
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public GetFinanialCategoriesQueryHandlerTests()
        {
            _configuration = new MapperConfiguration(
                config => config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnAllFinancialCategories()
        {
            DataSeeder.GetSampleFinancialCategories(DbContextOptions);
            var query = new GetFinancialCategoriesQuery();

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetFinancialCategoriesQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().HaveCount(DataSeeder.FinancialCategoriesCount);
            }
        }
    }
}

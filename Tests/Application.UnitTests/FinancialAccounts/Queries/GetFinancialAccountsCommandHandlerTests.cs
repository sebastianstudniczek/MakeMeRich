using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccounts;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Queries
{
    public class GetFinancialAccountsCommandHandlerTests : HandlerTest
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public GetFinancialAccountsCommandHandlerTests()
        {
            _configuration = new MapperConfiguration(
                config => config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnAllFinancialAccounts()
        {
            DataSeeder.GetSampleFinancialAccounts(DbContextOptions);
            var query = new GetFinancialAccountsQuery();

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetFinancialAccountsQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().HaveCount(3);
            }
        }
    }
}

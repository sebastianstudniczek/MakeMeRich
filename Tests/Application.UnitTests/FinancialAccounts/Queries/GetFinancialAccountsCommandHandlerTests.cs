using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Queries
{
    public class GetFinancialAccountsCommandHandlerTests : CommandHandlerTestBase
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public GetFinancialAccountsCommandHandlerTests()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnAllFinancialAccountsAndTransactions()
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

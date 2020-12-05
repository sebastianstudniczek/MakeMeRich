using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccounts;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Queries
{
    public class GetFinancialAccountsCommandHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public GetFinancialAccountsCommandHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
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

                result.Should().HaveCount(DataSeeder.FinancialAccountsCount);
            }
        }
    }
}

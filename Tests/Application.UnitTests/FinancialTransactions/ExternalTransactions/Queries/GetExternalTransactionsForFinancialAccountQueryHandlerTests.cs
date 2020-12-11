using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Queries.GetExternaltransactionsForFinancialAccountQuery;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Queries
{
    public class GetExternalTransactionsForFinancialAccountQueryHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public GetExternalTransactionsForFinancialAccountQueryHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldReturnExternalTransactionsForFinancialAccount()
        {
            DataSeeder.GetSampleExternalTransactions(DbContextOptions);
            var query = new GetExternalTransactionsForFinancialAccountQuery
            {
                FinancialAccountId = 2
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetExternalTransactionsForFinancialAccountQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeNull();
                result.ForEach(dto => dto.FinancialAccountId.Should().Be(2));
                result.Count.Should().Be(2);
            }
        }
    }
}

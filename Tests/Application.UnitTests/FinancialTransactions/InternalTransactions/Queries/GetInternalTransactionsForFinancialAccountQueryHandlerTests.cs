using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Queries;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Queries
{
    public class GetInternalTransactionsForFinancialAccountQueryHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public GetInternalTransactionsForFinancialAccountQueryHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldReturnInternalTransactionsForFinancialAccount()
        {
            DataSeeder.GetSampleInternalTransactions(DbContextOptions);
            var query = new GetInternalTransactionsForFinancialAccountQuery
            {
                FinancialAccountId = 2
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetInternalTransactionsForFinancialAccountQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeNull();
                result.Count.Should().Be(3);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Queries
{
    public class GetExternalTransactionByIdQueryHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public GetExternalTransactionByIdQueryHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldRequireValidExternalTransactionId()
        {
            var query = new GetExternalTransactionByIdQuery { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetExternalTransactionByIdQueryHandler(context, _mapper);

                FluentActions.Invoking(() => queryHandler.Handle(query, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldReturnExternalTransaction()
        {
            DataSeeder.GetSampleExternalTransactions(DbContextOptions);
            var query = new GetExternalTransactionByIdQuery { Id = 2 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetExternalTransactionByIdQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeNull();
                result.Should().BeOfType<ExternalTransactionDto>();
                result.Id.Should().Be(query.Id);
            }
        }
    }
}

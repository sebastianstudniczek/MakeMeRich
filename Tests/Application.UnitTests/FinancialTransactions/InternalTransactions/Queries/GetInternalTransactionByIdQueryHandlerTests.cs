using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Queries.GetInternalTransactionById;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Queries
{
    public class GetInternalTransactionByIdQueryHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public GetInternalTransactionByIdQueryHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldRequireValidInternalTransactionId()
        {
            var query = new GetInternalTransactionByIdQuery { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetInternalTransactionByIdQueryHandler(context, _mapper);

                FluentActions.Invoking(() => queryHandler.Handle(query, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldReturnInternalTransaction()
        {
            DataSeeder.GetSampleInternalTransactions(DbContextOptions);
            var query = new GetInternalTransactionByIdQuery { Id = 2 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetInternalTransactionByIdQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeNull();
                result.Should().BeOfType<InternalTransactionDto>();
                result.Id.Should().Be(query.Id);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.FinancialCategories.Queries.GetFinancialCategoryById;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Queries
{
    public class GetFinancialCategoryByIdQueryHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public GetFinancialCategoryByIdQueryHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldRequireValidFinancialCategoryId()
        {
            var query = new GetFinancialCategoryByIdQuery { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetFinancialCategoryByIdQueryHandler(context, _mapper);

                FluentActions.Invoking(() => queryHandler.Handle(query, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
        }

        [Fact]
        public async Task ShouldReturnFinancialCategory()
        {
            DataSeeder.GetSampleFinancialCategories(DbContextOptions);
            var query = new GetFinancialCategoryByIdQuery { Id = 2 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetFinancialCategoryByIdQueryHandler(context, _mapper);
                var dto = await queryHandler.Handle(query, CancellationToken.None);

                dto.Should().NotBeNull();
                dto.Id.Should().Be(query.Id);
            }
        }
    }
}

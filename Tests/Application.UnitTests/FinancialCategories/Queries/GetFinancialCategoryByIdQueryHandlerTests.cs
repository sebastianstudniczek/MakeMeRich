using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialCategories.Queries.GetFinancialCategoryById;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialCategories.Queries
{
    public class GetFinancialCategoryByIdQueryHandlerTests : HandlerTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public GetFinancialCategoryByIdQueryHandlerTests()
        {
            _configuration = new MapperConfiguration(
                config => config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
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
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeNull();
                result.Id.Should().Be(query.Id);
            }
        }
    }
}

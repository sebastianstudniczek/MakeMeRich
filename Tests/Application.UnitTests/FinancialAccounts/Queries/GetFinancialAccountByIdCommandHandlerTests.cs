using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById;
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

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Queries
{
    public class GetFinancialAccountByIdCommandHandlerTests : HandlerTest
    {
        private readonly MapperConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetFinancialAccountByIdCommandHandlerTests()
        {
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnFinancialAccount()
        {
            DataSeeder.GetSampleFinancialAccounts(DbContextOptions);
            var query = new GetFinancialAccountByIdQuery { Id = 2 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetFinancialAccountByIdQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, CancellationToken.None);

                result.Should().NotBeNull();
            }
        }
    }
}

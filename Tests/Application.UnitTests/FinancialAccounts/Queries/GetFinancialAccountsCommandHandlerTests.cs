using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Queries
{
    public class GetFinancialAccountsCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public GetFinancialAccountsCommandHandlerTests()
        {
            _configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>());
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task ShouldReturnAllFinancialAccountsAndTransactions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldReturnAllFinancialAccountsAndTransactions")
                .Options;

            DataSeeder.Seed(options);
            var query = new GetFinancialAccountsQuery();

            using (var context = new ApplicationDbContext(options))
            {
                var queryHandler = new GetFinancialAccountsQueryHandler(context, _mapper);
                var result = await queryHandler.Handle(query, new CancellationToken());

                result.FinancialAccounts.Should().HaveCount(3);
            }
        }
    }
}

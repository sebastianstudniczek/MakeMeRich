﻿using AutoMapper;

using FluentAssertions;

using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Queries
{
    public class GetFinancialAccountByIdCommandHandlerTests : HandlerTest
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public GetFinancialAccountByIdCommandHandlerTests()
        {
            _configuration = new MapperConfiguration(
                config => config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldRequireValidFinancialAccountId()
        {
            var query = new GetFinancialAccountByIdQuery { Id = 99 };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var queryHandler = new GetFinancialAccountByIdQueryHandler(context, _mapper);

                FluentActions.Invoking(() => queryHandler.Handle(query, CancellationToken.None))
                    .Should().Throw<NotFoundException>();
            }
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
                result.Should().BeOfType<FinancialAccountDto>();
                result.Id.Should().Be(query.Id);
            }
        }
    }
}

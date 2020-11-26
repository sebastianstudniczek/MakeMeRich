using System;

using AutoMapper;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactionCategories;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactions;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactionCategories;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using Xunit;

namespace MakeMeRich.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(FinancialAccount), typeof(FinancialAccountDto))]
        [InlineData(typeof(FinancialCategory), typeof(FinancialCategoryDto))]
        [InlineData(typeof(ExternalTransaction), typeof(ExternalTransactionDto))]
        [InlineData(typeof(InternalTransaction), typeof(InternalTransactionDto))]
        [InlineData(typeof(ExternalTransactionCategory), typeof(ExternalTransactionCategoryDto))]
        [InlineData(typeof(InternalTransactionCategory), typeof(InternalTransactionCategoryDto))]
        public void ShouldMapFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);
            _mapper.Map(instance, source, destination);
        }
    }
}

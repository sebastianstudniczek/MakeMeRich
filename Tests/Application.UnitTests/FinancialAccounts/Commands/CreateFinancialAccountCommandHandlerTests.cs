using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class CreateFinancialAccountCommandHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateFinancialAccountCommandHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldCreateFinancialAccount()
        {
            FinancialAccountDto dto;
            var command = new CreateFinancialAccountCommand
            {
                Title = "BNP Private",
                CurrentBalance = 250,
                Type = FinancialAccountType.Banking
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new CreateFinancialAccountCommandHandler(context, _mapper);
                dto = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var financialAccount = await context.FindAsync<FinancialAccount>(dto.Id);

                financialAccount.Should().NotBeNull();
                financialAccount.Title.Should().Be(command.Title);
                financialAccount.CurrentBalance.Should().Be(command.CurrentBalance);
                financialAccount.AccountType.Should().Be(command.Type);
            }
        }
    }
}

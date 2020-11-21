using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Moq;

using Xunit;

namespace MakeMeRich.Application.IntegrationTests.FinancialAccounts.Commands
{
    public class CreateFinancialAccountCommandHandlerTests
    {
        private readonly CreateFinancialAccountCommandHandler _createFinancialAccountCommandHandler;
        private readonly ApplicationDbContext _context;

        public CreateFinancialAccountCommandHandlerTests()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "createFinancialAccountCommandHandlerTest")
                .Options ;

            _context = new ApplicationDbContext(ContextOptions);
            _createFinancialAccountCommandHandler =
                 new CreateFinancialAccountCommandHandler(_context);
        }

        public DbContextOptions<ApplicationDbContext> ContextOptions { get; set; }

        [Fact]
        public async Task ShouldCreateFinancialAccount()
        {
            var command = new CreateFinancialAccountCommand
            {
                Title = "BNP Osobiste",
                CurrentBalance = 250
            };

            int id = await _createFinancialAccountCommandHandler.Handle(command, new CancellationToken());
            var financialAccount = await _context.FindAsync<FinancialAccount>(id);

            financialAccount.Should().NotBeNull();
            financialAccount.Title.Should().Be(command.Title);
            financialAccount.CurrentBalance.Should().Be(250);
        }
    }
}

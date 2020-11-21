using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialAccounts.Commands
{
    public class UpdateFinancialAccountCommandHandlerTests
    {
        [Fact]
        public async Task ShouldUpdateFinancialAccount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "updateFinancialAccountCommandHandler")
                .Options;

            var createCommand = new CreateFinancialAccountCommand
            {
                Title = "ING Bank",
                CurrentBalance = 500
            };

            int financialAccountId;

            using(var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new CreateFinancialAccountCommandHandler(context);

                financialAccountId = await commandHandler.Handle(createCommand, new CancellationToken());
            }

            var updateCommand = new UpdateFinancialAccountCommand
            {
                Id = financialAccountId,
                Title = "ING Bank Śląski",
                CurrentBalance = 1500
            };

            using (var context = new ApplicationDbContext(options))
            {
                var commandHandler =
                    new UpdateFinancialAccountCommandHandler(context);

                await commandHandler.Handle(updateCommand, new CancellationToken());
                var financialAccount = await context.FindAsync<FinancialAccount>(financialAccountId);

                financialAccount.Title.Should().Be(updateCommand.Title);
                financialAccount.CurrentBalance.Should().Be(updateCommand.CurrentBalance);
            }
        }
    }
}

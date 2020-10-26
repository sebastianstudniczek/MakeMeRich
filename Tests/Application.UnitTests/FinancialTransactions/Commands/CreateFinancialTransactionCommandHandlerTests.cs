using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.Commands
{
    public class CreateFinancialTransactionCommandHandlerTests
    {
        [Fact]
        public async Task ShouldCreateFinancialTransaction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldCreateFinancialTransaction")
                .Options;

            var command = new CreateFinancialTransactionCommand();
        }
    }
}

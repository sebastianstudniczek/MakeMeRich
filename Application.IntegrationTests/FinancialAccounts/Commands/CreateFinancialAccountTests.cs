using System;
using System.Collections.Generic;
using System.Text;

using MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount;

using Xunit;

namespace MakeMeRich.Application.IntegrationTests.FinancialAccounts.Commands
{
    public class CreateFinancialAccountTests
    {
        [Fact]
        public void ShouldCreateFinancialAccount()
        {
            // Arrange
            var command = new CreateFinancialAccountCommand
            {

            }
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.ExternalTransactions.Commands
{
    public class CreateExternalTransactionCommandHandlerTests : HandlerTest
    {
        private readonly IMapper _mapper;

        public CreateExternalTransactionCommandHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldCreateExternalTransaction()
        {
            ExternalTransactionDto dto;
            var command = new CreateExternalTransactionCommand
            {
                TransactionSideName = "Allegro",
                TotalAmount = 526,
                DueDate = new DateTime(2019, 10, 26),
                Description = "Sample shopping",
                Type = ExternalTransactionType.Expense,
                SendingAccountId = 1
                // TODO: Categories
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new CreateExternalTransactionCommandHandler(context, _mapper);
                dto = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var externalTransaction = await context.FindAsync<ExternalTransaction>(dto.Id);

                externalTransaction.Should().NotBeNull();
                externalTransaction.TransactionSideName.Should().Be(command.TransactionSideName);
                externalTransaction.TotalAmount.Should().Be(command.TotalAmount);
                externalTransaction.DueDate.Should().Be(command.DueDate);
                externalTransaction.Description.Should().Be(command.Description);
                externalTransaction.TransactionType.Should().Be(command.Type);
            }
        }
    }
}

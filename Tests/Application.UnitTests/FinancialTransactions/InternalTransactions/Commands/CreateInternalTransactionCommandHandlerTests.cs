using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.UnitTests.Common;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Infrastructure.Persistance;
using Xunit;

namespace MakeMeRich.Application.UnitTests.FinancialTransactions.InternalTransactions.Commands
{
    public class CreateInternalTransactionCommandHandlerTests : HandlerTest, IClassFixture<DtoResponseHandlerTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateInternalTransactionCommandHandlerTests(DtoResponseHandlerTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldCreateInternalTransaction()
        {
            InternalTransactionDto dto;
            var command = new CreateInternalTransactionCommand
            {
                TotalAmount = 255,
                Description = "Some internal transaction",
                DueDate = new DateTime(2016, 7, 12),
                SendingAccountId = 1,
                ReceivingAccountId = 2
            };

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var commandHandler = new CreateInternalTransactionCommandHandler(context, _mapper);
                dto = await commandHandler.Handle(command, CancellationToken.None);
            }

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                var internalTransaction = await context.FindAsync<InternalTransaction>(dto.Id);

                internalTransaction.Should().NotBeNull();
                internalTransaction.TotalAmount.Should().Be(command.TotalAmount);
                internalTransaction.Description.Should().Be(command.Description);
                internalTransaction.DueDate.Should().Be(command.DueDate);
                internalTransaction.SendingAccountId.Should().Be(command.SendingAccountId);
                internalTransaction.ReceivingAccountId.Should().Be(command.ReceivingAccountId);
            }
        }
    }
}

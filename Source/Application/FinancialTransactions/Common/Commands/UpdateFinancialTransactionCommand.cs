using System;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public class UpdateFinancialTransactionCommand : IRequest
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}

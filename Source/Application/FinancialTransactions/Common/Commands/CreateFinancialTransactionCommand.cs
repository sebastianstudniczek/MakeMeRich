using System;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public abstract class CreateFinancialTransactionCommand : IRequest<int>
    {
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public int FinancialAccountId { get; set; }
    }
}

using System;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public abstract class CreateFinancialTransactionCommand
    {
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public int SendingAccountId { get; set; }
    }
}

using System;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public class UpdateFinancialTransactionCommand
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}

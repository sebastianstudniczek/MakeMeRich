using System;
using System.ComponentModel.DataAnnotations;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public abstract class CreateFinancialTransactionCommand
    {
        public double TotalAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public int FinancialAccountId { get; set; }
    }
}

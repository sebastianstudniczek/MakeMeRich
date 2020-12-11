using System;
using System.ComponentModel.DataAnnotations;

namespace MakeMeRich.Application.FinancialTransactions.Common.Commands
{
    public abstract class UpdateFinancialTransactionCommand
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}

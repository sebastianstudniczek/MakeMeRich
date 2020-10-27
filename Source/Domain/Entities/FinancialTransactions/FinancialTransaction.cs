using System;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public abstract class FinancialTransaction : BaseEntity
    {
        public int FinancialAccountId { get; set; }
        public double Amount { get; set; }
        public string Memo { get; set; }
        public DateTime DueDate { get; set; }
    }
}

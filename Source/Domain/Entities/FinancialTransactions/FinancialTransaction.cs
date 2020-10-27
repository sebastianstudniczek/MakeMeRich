using System;
using System.Collections.Generic;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public abstract class FinancialTransaction : BaseEntity
    {
        public int FinancialAccountId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public ICollection<TransactionCategory> TransactionCategories { get; set; }
            = new HashSet<TransactionCategory>();
    }
}

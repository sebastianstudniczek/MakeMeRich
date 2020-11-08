using System;
using System.Collections.Generic;

using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public abstract class FinancialTransaction : BaseEntity
    {
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }

        public int FinancialAccountId { get; set; }
        public FinancialAccount FinancialAccount { get; set; }
    }
}

using System;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public abstract class FinancialTransaction : BaseEntity
    {
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}

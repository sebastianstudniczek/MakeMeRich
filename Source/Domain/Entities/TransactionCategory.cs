using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Domain.Entities
{
    public class TransactionCategory : BaseEntity
    {
        public double Amount { get; set; }
        public string Description { get; set; }

        public int FinancialTransactionId { get; set; }
        public FinancialTransaction Transaction { get; set; }
        public int FinancialCategoryId { get; set; }
        public FinancialCategory Category { get; set; }
    }
}

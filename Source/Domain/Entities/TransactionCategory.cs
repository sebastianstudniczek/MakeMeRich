using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Domain.Entities
{
    public class TransactionCategory
    {
        public int FinancialTransactionId { get; set; }
        public int FinancialCategoryId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public FinancialTransaction Transaction { get; set; }
        public FinancialCategory Category { get; set; }
    }
}

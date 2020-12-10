using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Domain.Entities
{
    public class ExternalTransactionCategory
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int FinancialCategoryId { get; set; }

        public FinancialCategory FinancialCategory { get; set; }
        public int ExternalTransactionId { get; set; }
        public ExternalTransaction ExternalTransaction { get; set; }
    }
}

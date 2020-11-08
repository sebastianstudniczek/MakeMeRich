using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Domain.Entities.FinancialTransactionCategories
{
    public class ExternalTransactionCategory : FinancialTransactionCategory
    {
        public int ExternalTransactionId { get; set; }
        public ExternalTransaction ExternalTransaction { get; set; }
    }
}

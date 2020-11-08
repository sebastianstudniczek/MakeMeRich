using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Domain.Entities.FinancialTransactionCategories
{
    public class InternalTransactionCategory : FinancialTransactionCategory
    {
        public int InternalTransactionId { get; set; }
        public InternalTransaction InternalTransaction { get; set; }
    }
}

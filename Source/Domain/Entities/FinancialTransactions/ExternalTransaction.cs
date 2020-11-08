using System.Collections.Generic;

using MakeMeRich.Domain.Entities.FinancialTransactionCategories;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class ExternalTransaction : FinancialTransaction
    {
        public string TransactionSideName { get; set; }
        public ExternalTransactionType Type { get; set; }
        public ICollection<ExternalTransactionCategory> TransactionCategories { get; set; }
            = new HashSet<ExternalTransactionCategory>();
    }
}

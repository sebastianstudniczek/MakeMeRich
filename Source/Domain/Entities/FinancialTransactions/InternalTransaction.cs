using System.Collections;
using System.Collections.Generic;

using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class InternalTransaction : FinancialTransaction
    {
        public int ReceivingAccountId { get; set; }
        public FinancialAccount ReceivingAccount { get; set; }
        public ICollection<InternalTransactionCategory> TransactionCategories { get; private set; }
            = new HashSet<InternalTransactionCategory>();
    }
}

using System.Collections.Generic;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<TransactionCategory> CategoryTransactions { get; private set; }
            = new HashSet<TransactionCategory>();
    }
}

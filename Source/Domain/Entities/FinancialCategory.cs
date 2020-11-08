using System.Collections.Generic;

using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<FinancialTransactionCategory> CategoryTransactions { get; private set; }
            = new HashSet<FinancialTransactionCategory>();
    }
}

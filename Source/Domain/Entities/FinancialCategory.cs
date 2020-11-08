using System.Collections.Generic;

using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ExternalTransactionCategory> ExternalTransactionCategories { get; set; }
            = new HashSet<ExternalTransactionCategory>();
        public ICollection<InternalTransactionCategory> InternalTransactionCategories { get; set; }
            = new HashSet<InternalTransactionCategory>();
    }
}

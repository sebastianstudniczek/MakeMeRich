using System.Collections.Generic;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ExternalTransactionCategory> ExternalTransactionCategories { get; set; }
            = new HashSet<ExternalTransactionCategory>();
    }
}

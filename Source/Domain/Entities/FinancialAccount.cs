using System.Collections.Generic;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialAccount : BaseEntity
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public ICollection<FinancialTransaction> Transactions { get; set; }
        = new HashSet<FinancialTransaction>();
    }
}

using System.Collections.Generic;

using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialAccount : BaseEntity
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public FinancialAccountType Type { get; set; }
        public ICollection<ExternalTransaction> ExternalTransactions { get; private set; }
            = new HashSet<ExternalTransaction>();
        public ICollection<InternalTransaction> InternalTransactions { get; private set; }
            = new HashSet<InternalTransaction>();

    }
}

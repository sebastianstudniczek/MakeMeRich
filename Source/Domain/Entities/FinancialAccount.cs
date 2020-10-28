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
        public ICollection<FinancialTransaction> Transactions { get; set; }
        = new HashSet<FinancialTransaction>();
    }
}

using System.Collections.Generic;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialAccount : BaseEntity
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public FinancialAccountType AccountType { get; set; }

        public ICollection<ExternalTransaction> ExternalTransactions { get; }
            = new HashSet<ExternalTransaction>();
        public ICollection<InternalTransaction> ReceivedInternalTransactions { get; }
            = new HashSet<InternalTransaction>();
        public ICollection<InternalTransaction> SendedInternalTransactions { get; }
            = new HashSet<InternalTransaction>();
    }
}

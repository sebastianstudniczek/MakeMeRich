﻿using System.Collections.Generic;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class ExternalTransaction : FinancialTransaction
    {
        public string TransactionSideName { get; set; }
        public ExternalTransactionType TransactionType { get; set; }
        public int FinancialAccountId { get; set; }
        public FinancialAccount FinancialAccount { get; set; }
        public ICollection<ExternalTransactionCategory> TransactionCategories { get; }
            = new HashSet<ExternalTransactionCategory>();
    }
}

﻿using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class ExternalTransaction : FinancialTransaction
    {
        public string TransactionSideName { get; set; }
        public ExternalFinancialTransactionType Type { get; set; }
    }
}
﻿using System.Collections.Generic;

namespace MakeMeRich.Domain.Entities
{
    public class FinancialAccount : BaseEntity
    {
        public FinancialAccount()
        {
            Transactions = new List<FinancialTransaction>();
        }
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public IList<FinancialTransaction> Transactions { get; set; }
    }
}
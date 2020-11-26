using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactionCategories;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactions
{
    public class InternalTransactionDto : IMapFrom<InternalTransaction>
    {
        public int Id { get; init; }
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
        public double TotalAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public IList<InternalTransactionCategoryDto> TransactionCategories { get; set; }
            = new List<InternalTransactionCategoryDto>();
    }
}

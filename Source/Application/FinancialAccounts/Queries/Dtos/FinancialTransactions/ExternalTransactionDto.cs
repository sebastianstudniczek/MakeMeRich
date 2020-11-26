using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactionCategories;
using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactions
{
    public class ExternalTransactionDto : IMapFrom<ExternalTransaction>
    {
        public int Id { get; init; }
        public int FinancialAccountId { get; set; }
        public string TransactionSideName { get; set; }
        public double TotalAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public IList<ExternalTransactionCategoryDto> TransactionCategories { get; set; }
            = new List<ExternalTransactionCategoryDto>();
    }
}
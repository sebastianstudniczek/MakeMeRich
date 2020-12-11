using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MakeMeRich.Application.Common.CustomConverters;
using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Application.Common.Dtos.FinancialTransactions
{
    public class ExternalTransactionDto : IMapFrom<ExternalTransaction>
    {
        public int Id { get; set; }
        public int FinancialAccountId { get; set; }
        public string TransactionSideName { get; set; }
        public string TransactionType { get; set; }
        public double TotalAmount { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public IList<ExternalTransactionCategoryDto> TransactionCategories { get; set; }
            = new List<ExternalTransactionCategoryDto>();
    }
}
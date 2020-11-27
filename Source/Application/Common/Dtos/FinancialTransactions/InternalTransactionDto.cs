using MakeMeRich.Application.Common.CustomConverters;
using MakeMeRich.Application.Common.Dtos.FinancialTransactionCategories;
using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MakeMeRich.Application.Common.Dtos.FinancialTransactions
{
    public class InternalTransactionDto : IMapFrom<InternalTransaction>
    {
        public int Id { get; init; }
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
        public double TotalAmount { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public IList<InternalTransactionCategoryDto> TransactionCategories { get; set; }
            = new List<InternalTransactionCategoryDto>();
    }
}

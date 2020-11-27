using System.Collections.Generic;

using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities;

namespace MakeMeRich.Application.Common.Dtos
{
    public class FinancialAccountDto : IMapFrom<FinancialAccount>
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public string AccountType { get; set; }
        public IList<ExternalTransactionDto> ExternalTransactions { get; set; }
            = new List<ExternalTransactionDto>();
        public IList<InternalTransactionDto> ReceivedInternalTransactions { get; set; }
            = new List<InternalTransactionDto>();
        public IList<InternalTransactionDto> SendedInternalTransactions { get; set; }
            = new List<InternalTransactionDto>();
    }
}
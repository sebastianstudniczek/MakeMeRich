using System.Collections.Generic;

using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class FinancialAccountDto : IMapFrom<FinancialAccount>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public IList<FinancialTransactionDto> Transactions { get; set; } = new List<FinancialTransactionDto>();
    }
}
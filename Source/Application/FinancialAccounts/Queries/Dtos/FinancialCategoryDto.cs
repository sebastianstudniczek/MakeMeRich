using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactionCategories;
using MakeMeRich.Domain.Entities;

using System.Collections.Generic;

namespace MakeMeRich.Application.FinancialAccounts.Queries.Dtos
{
    public class FinancialCategoryDto : IMapFrom<FinancialCategory>
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public IList<ExternalTransactionCategoryDto> ExternalTransactionCategories { get; set; }
            = new List<ExternalTransactionCategoryDto>();
        public IList<InternalTransactionCategoryDto> InternalTransactionCategories { get; set; }
            = new List<InternalTransactionCategoryDto>();
    }
}

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
    }
}
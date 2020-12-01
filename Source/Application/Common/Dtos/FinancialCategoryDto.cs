using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities;

namespace MakeMeRich.Application.Common.Dtos
{
    public class FinancialCategoryDto : IMapFrom<FinancialCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

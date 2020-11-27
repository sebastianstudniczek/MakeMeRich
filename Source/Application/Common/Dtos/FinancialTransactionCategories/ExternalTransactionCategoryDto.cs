using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

namespace MakeMeRich.Application.Common.Dtos.FinancialTransactionCategories
{
    public class ExternalTransactionCategoryDto : IMapFrom<ExternalTransactionCategory>
    {
        public int ExternalTransactionId { get; set; }
        public int FinancialCategoryId { get; set; }
        public string FinancialCategoryName { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<ExternalTransactionCategory, ExternalTransactionCategoryDto>()
                .ForMember(dest => dest.FinancialCategoryName, opt => opt.MapFrom(source => source.FinancialCategory.Name));
        }
    }
}
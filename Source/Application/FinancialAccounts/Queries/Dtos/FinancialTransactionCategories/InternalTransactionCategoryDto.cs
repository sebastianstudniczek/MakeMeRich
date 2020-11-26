using MakeMeRich.Application.Common.Mappings;
using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

namespace MakeMeRich.Application.FinancialAccounts.Queries.Dtos.FinancialTransactionCategories
{
    public class InternalTransactionCategoryDto : IMapFrom<InternalTransactionCategory>
    {
        public int InternalTransactionId { get; set; }
        public int FinancialCategoryId { get; set; }
        public string FinancialCategoryName { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<InternalTransactionCategory, InternalTransactionCategoryDto>()
                .ForMember(dest => dest.FinancialCategoryName, opt => opt.MapFrom(source => source.FinancialCategory.Name));
        }
    }
}

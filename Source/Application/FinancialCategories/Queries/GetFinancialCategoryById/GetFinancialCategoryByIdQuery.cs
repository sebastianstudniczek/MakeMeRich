using MakeMeRich.Application.Common.Dtos;
using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Queries.GetFinancialCategoryById
{
    public class GetFinancialCategoryByIdQuery : IRequest<FinancialCategoryDto>
    {
        public int Id { get; set; }
    }
}

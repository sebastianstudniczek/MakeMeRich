using MakeMeRich.Application.Common.Dtos;
using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory
{
    public class CreateFinancialCategoryCommand : IRequest<FinancialCategoryDto>
    {
        public string Name { get; set; }
    }
}


using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory
{
    public class CreateFinancialCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}

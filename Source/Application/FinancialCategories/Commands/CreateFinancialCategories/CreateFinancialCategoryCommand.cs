using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategories
{
    public class CreateFinancialCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}

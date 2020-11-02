using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory
{
    public class UpdateFinancialCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

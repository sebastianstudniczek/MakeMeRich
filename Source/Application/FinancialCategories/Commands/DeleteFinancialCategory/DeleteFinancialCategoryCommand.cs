using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.DeleteFinancialCategory
{
    public class DeleteFinancialCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}

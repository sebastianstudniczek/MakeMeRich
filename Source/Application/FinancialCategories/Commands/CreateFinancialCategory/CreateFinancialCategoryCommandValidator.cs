using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MakeMeRich.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory
{
    public class CreateFinancialCategoryCommandValidator : AbstractValidator<CreateFinancialCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateFinancialCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(75).WithMessage("Title must not exceed 75 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("Financial category with given name already exists");
        }

        public async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
        {
            return await _context.FinancialCategories
                .AllAsync(category => category.Name != name, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

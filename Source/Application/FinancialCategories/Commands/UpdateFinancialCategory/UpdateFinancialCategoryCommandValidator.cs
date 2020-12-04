using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MakeMeRich.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory
{
    public class UpdateFinancialCategoryCommandValidator : AbstractValidator<UpdateFinancialCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFinancialCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(80).WithMessage("Name must not exceed 80 characters")
                .MustAsync(BeUniqueName).WithMessage("The specified name is already taken.");
        }

        public async Task<bool> BeUniqueName(UpdateFinancialCategoryCommand command, string name, CancellationToken cancellationToken )
        {
            return await _context.FinancialCategories
                .Where(category => category.Id != command.Id)
                .AllAsync(category => category.Name != name, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MakeMeRich.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount
{
    public class UpdateFinancialAccountCommandValidator : AbstractValidator<UpdateFinancialAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFinancialAccountCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(command => command.Title)
                  .NotEmpty().WithMessage("Title is required")
                  .MaximumLength(150).WithMessage("Title must not exceed 200 characters.")
                  .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.FinancialAccounts
                .AllAsync(account => account.Title != title);
        }
    }
}

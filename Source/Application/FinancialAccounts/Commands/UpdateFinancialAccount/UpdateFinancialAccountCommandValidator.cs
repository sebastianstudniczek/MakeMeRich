using System.Linq;
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
                  .NotEmpty().WithMessage("Title is required.")
                  .MaximumLength(150)
                  .MustAsync(BeUniqueTitle).WithMessage("Financial account with the specified title already exists.");
        }

        public Task<bool> BeUniqueTitle(UpdateFinancialAccountCommand command, string title, CancellationToken cancellationToken)
        {
            return _context.FinancialAccounts
                .Where(account => account.Id != command.Id)
                .AllAsync(account => account.Title != title, cancellationToken);
        }
    }
}

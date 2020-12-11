using FluentValidation;
using MakeMeRich.Application.Common.Interfaces;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction
{
    public class UpdateExternalTransactionCommandValidator : AbstractValidator<UpdateExternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateExternalTransactionCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(command => command.TotalAmount)
                .NotEmpty().WithMessage("Total amount is required.")
                .GreaterThan(0);

            RuleFor(command => command.DueDate)
                .NotEmpty().WithMessage("Due date is required and cannot have default value.");

            RuleFor(command => command.Description)
                .MaximumLength(150);

            RuleFor(command => command.FinancialAccountId)
                .NotEmpty().WithMessage("Financial account id is required.");

            RuleFor(command => command.TransactionSideName)
                .NotEmpty().WithMessage("Transaction side name is required.")
                .MaximumLength(80);

            RuleFor(command => command.TransactionType)
                .IsInEnum();

            RuleFor(command => command.TransactionCategories)
                .NotEmpty().WithMessage("At least 1 category is required.");

            RuleForEach(command => command.TransactionCategories)
                .SetValidator(new ExternalTransactionCategoryUpdateDtoValidator(_context));
        }
    }
}

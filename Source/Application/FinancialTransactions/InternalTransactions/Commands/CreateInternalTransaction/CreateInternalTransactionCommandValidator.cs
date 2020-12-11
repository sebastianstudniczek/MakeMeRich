using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MakeMeRich.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.CreateInternalTransaction
{
    public class CreateInternalTransactionCommandValidator : AbstractValidator<CreateInternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateInternalTransactionCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(command => command.TotalAmount)
                .NotEmpty().WithMessage("Total amount is required.")
                .GreaterThan(0);

            RuleFor(command => command.DueDate)
                .NotEmpty().WithMessage("Due date is required and cannot have default value.");

            RuleFor(command => command.Description)
                .MaximumLength(150);

            RuleFor(command => command.SendingAccountId)
                .NotEmpty().WithMessage("Financial account id is required.");

            RuleFor(command => command.ReceivingAccountId)
                .NotEmpty().WithMessage("Receiving account id is required.")
                .MustAsync(Exist).WithMessage("Financial account (receiving account) with the given id doesn't exists.");
        }

        public Task<bool> Exist(int id, CancellationToken cancellationToken)
        {
            return _context.FinancialAccounts
                .AnyAsync(account => account.Id == id, cancellationToken);
        }
    }
}

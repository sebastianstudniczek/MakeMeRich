using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MakeMeRich.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction
{
    public class ExternalTransactionCategoryUpdateDtoValidator : AbstractValidator<ExternalTransactionCategoryUpdateDto>
    {
        private readonly IApplicationDbContext _context;

        public ExternalTransactionCategoryUpdateDtoValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(dto => dto.FinancialCategoryId)
                .NotEmpty().WithMessage("Financial category id is required.")
                .MustAsync(Exist).WithMessage("Financial category with the given id doesn't exists.");

            RuleFor(dto => dto.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be positive.");

            RuleFor(dto => dto.Description)
                .MaximumLength(150);
        }

        public Task<bool> Exist(int id, CancellationToken cancellationToken)
        {
            return _context.FinancialCategories
                .AnyAsync(category => category.Id == id, cancellationToken);
        }
    }
}

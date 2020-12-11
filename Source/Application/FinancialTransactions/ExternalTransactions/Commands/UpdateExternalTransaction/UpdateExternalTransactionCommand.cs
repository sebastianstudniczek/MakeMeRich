using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction
{
    public class UpdateExternalTransactionCommand : UpdateFinancialTransactionCommand, IRequest
    {
        public int FinancialAccountId { get; set; }
        public string TransactionSideName { get; set; }
        public ExternalTransactionType TransactionType { get; set; }
        public IEnumerable<ExternalTransactionCategoryUpdateDto> TransactionCategories { get; set; }
    }

    public class UpdateExternalTransactionCommandHandler : IRequestHandler<UpdateExternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateExternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExternalTransactions.FindAsync(request.Id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExternalTransaction), request.Id);
            }

            entity.TransactionSideName = request.TransactionSideName;
            entity.TotalAmount = request.TotalAmount;
            entity.DueDate = request.DueDate;
            entity.Description = request.Description;
            entity.TransactionType = request.TransactionType;
            entity.FinancialAccountId = request.FinancialAccountId;
            entity.TransactionCategories.Clear();

            foreach (var transactionCategory in request.TransactionCategories)
            {
                entity.TransactionCategories.Add(
                    new ExternalTransactionCategory
                    {
                        FinancialCategoryId = transactionCategory.FinancialCategoryId,
                        Amount = transactionCategory.Amount,
                        Description = transactionCategory.Description
                    });
            }

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

}
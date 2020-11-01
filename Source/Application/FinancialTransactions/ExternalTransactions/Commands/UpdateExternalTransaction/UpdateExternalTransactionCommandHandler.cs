using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction
{
    public class UpdateExternalTransactionCommandHandler : IRequestHandler<UpdateExternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateExternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExternalTransactions.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExternalTransaction), request.Id);
            }

            entity.TransactionSideName = request.TransactionSideName;
            entity.TotalAmount = request.TotalAmount;
            entity.DueDate = request.DueDate;
            entity.Description = request.Description;
            entity.Type = request.Type;
            entity.FinancialAccountId = request.FinancialAccountId;
            entity.FinancialAccount = await _context.FinancialAccounts.FindAsync(request.FinancialAccountId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
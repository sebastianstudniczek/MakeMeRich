using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction
{
    public class DeleteExternalTransactionCommandHandler : IRequestHandler<DeleteExternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteExternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExternalTransactions
                .Where(transaction => transaction.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExternalTransaction), request.Id);
            }

            _context.ExternalTransactions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

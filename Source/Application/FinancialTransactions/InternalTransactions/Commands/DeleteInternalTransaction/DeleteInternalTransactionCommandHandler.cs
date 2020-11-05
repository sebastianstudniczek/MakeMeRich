using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.DeleteInternalTransaction
{
    public class DeleteInternalTransactionCommandHandler : IRequestHandler<DeleteInternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.InternalTransactions.FindAsync(request.Id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(InternalTransaction), request.Id);
            }

            _context.InternalTransactions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

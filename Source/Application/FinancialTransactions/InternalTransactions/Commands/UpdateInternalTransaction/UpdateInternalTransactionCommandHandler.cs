using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.UpdateInternalTransaction;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using MediatR;

namespace MakeMeRich.Application
{
    public class UpdateInternalTransactionCommandHandler : IRequestHandler<UpdateInternalTransactionCommand>
    {
        private IApplicationDbContext _context;

        public UpdateInternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.InternalTransactions.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(InternalTransaction), request.Id);
            }

            entity.TotalAmount = request.TotalAmount;
            entity.DueDate = request.DueDate;
            entity.Description = request.Description;
            entity.FinancialAccountId = request.FinancialAccountId;
            entity.ReceivingAccountId = request.ReceivingAccountId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
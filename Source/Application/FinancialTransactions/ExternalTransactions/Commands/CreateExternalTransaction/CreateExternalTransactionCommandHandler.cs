using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction
{
    public class CreateExternalTransactionCommandHandler : IRequestHandler<CreateExternalTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateExternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new ExternalTransaction
            {
                TransactionSideName = request.TransactionSideName,
                Type = request.Type,
                TotalAmount = request.TotalAmount,
                DueDate = request.DueDate,
                Description = request.Description
            };

            _context.ExternalTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity.Id;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;

namespace MakeMeRich.Application
{
    public class CreateInternalTransactionCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreateInternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new InternalTransaction
            {
                TotalAmount = request.TotalAmount,
                DueDate = request.DueDate,
                Description = request.Description,
                SendingAccountId = request.SendingAccountId,
                ReceivingAccountId = request.ReceivingAccountId
                // TODO: Categories
            };

            _context.InternalTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity.Id;
        }
    }
}
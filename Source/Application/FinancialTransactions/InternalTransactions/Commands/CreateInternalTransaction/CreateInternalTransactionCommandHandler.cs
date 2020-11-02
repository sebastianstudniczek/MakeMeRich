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

        public async Task<int> Handle(CreateInternalTransactionCommand command, CancellationToken cancellationToken)
        {
            var entity = new InternalTransaction
            {
                TotalAmount = command.TotalAmount,
                DueDate = command.DueDate,
                Description = command.Description,
                FinancialAccountId = command.FinancialAccountId,
                ReceivingAccountId = command.ReceivingAccountId
                // TODO: Categories
            };

            _context.InternalTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
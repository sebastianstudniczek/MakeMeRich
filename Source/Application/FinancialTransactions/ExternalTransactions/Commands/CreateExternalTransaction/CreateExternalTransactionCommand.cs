using System.Threading;
using System.Threading.Tasks;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;
using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction
{
    public class CreateExternalTransactionCommand : CreateFinancialTransactionCommand
    {
        public string TransactionSideName { get; set; }
        public ExternalTransactionType Type { get; set; }
    }

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
                TransactionType = request.Type,
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

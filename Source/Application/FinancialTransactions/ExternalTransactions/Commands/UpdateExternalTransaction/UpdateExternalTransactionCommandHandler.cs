using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

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
            throw new System.NotImplementedException();
        }
    }
}
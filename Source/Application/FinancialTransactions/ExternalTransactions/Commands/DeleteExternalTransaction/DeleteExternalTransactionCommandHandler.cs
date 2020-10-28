using System;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction
{
    public class DeleteExternalTransactionCommandHandler : IRequestHandler<DeleteExternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteExternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Unit> Handle(DeleteExternalTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

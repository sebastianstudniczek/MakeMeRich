using System;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

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

        public Task<Unit> Handle(DeleteInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

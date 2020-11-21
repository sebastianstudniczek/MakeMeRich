using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount
{
    public class DeleteFinancialAccountCommandHandler : IRequestHandler<DeleteFinancialAccountCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteFinancialAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialAccounts
                .Where(account => account.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {

            }
        }
    }
}

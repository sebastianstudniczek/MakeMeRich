using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

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
                throw new NotFoundException(nameof(FinancialAccount), request.Id);
            }

            _context.FinancialAccounts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

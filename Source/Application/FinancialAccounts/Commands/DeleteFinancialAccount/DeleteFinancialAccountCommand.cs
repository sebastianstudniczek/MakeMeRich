using System.Threading;
using System.Threading.Tasks;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;
using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount
{
    public class DeleteFinancialAccountCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteFinancialAccountCommandHandler : IRequestHandler<DeleteFinancialAccountCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteFinancialAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialAccounts.FindAsync(request.Id).ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(FinancialAccount), request.Id);
            }

            _context.FinancialAccounts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

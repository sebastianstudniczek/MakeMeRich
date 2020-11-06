using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.DeleteFinancialCategory
{
    public class DeleteFinancialCategoryCommandHandler : IRequestHandler<DeleteFinancialCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFinancialCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFinancialCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialCategories.FindAsync(request.Id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FinancialCategory), request.Id);
            }

            _context.FinancialCategories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

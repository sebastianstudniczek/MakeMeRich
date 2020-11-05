using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.UpdateFinancialCategory
{
    public class UpdateFinancialCategoryCommandHandler : IRequestHandler<UpdateFinancialCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFinancialCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFinancialCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialCategories.FindAsync(request.Id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FinancialCategory), request.Id);
            }

            entity.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}

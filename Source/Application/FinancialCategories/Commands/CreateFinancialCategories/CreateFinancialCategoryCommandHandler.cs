using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategories
{
    public class CreateFinancialCategoryCommandHandler : IRequestHandler<CreateFinancialCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateFinancialCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFinancialCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new FinancialCategory
            {
                Name = request.Name
            };

            _context.FinancialCategories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return entity.Id;
        }
    }
}

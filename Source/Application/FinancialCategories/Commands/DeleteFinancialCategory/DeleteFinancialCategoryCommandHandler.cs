using System;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

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

        public Task<Unit> Handle(DeleteFinancialCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

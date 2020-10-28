using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommandHandler : IRequestHandler<CreateFinancialAccountCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateFinancialAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new FinancialAccount()
            {
                Title = request.Title,
                CurrentBalance = request.CurrentBalance,
                Type = request.Type
            };

            _context.FinancialAccounts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount
{
    public class UpdateFinancialAccountCommandHandler : IRequestHandler<UpdateFinancialAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFinancialAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialAccounts.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FinancialAccount), request.Id);
            }

            entity.Title = request.Title;
            entity.CurrentBalance = request.CurrentBalance;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

﻿using System.Threading;
using System.Threading.Tasks;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.UpdateInternalTransaction
{
    public class UpdateInternalTransactionCommand : UpdateFinancialTransactionCommand, IRequest
    {
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
    }

    public class UpdateInternalTransactionCommandHandler : IRequestHandler<UpdateInternalTransactionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.InternalTransactions.FindAsync(request.Id).ConfigureAwait(false);

            if (entity == null)
            {
                throw new NotFoundException(nameof(InternalTransaction), request.Id);
            }

            entity.TotalAmount = request.TotalAmount;
            entity.DueDate = request.DueDate;
            entity.Description = request.Description;
            entity.SendingAccountId = request.SendingAccountId;
            entity.ReceivingAccountId = request.ReceivingAccountId;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
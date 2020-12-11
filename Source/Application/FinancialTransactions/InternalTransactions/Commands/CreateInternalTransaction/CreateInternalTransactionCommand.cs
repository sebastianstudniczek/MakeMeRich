using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MediatR;

namespace MakeMeRich.Application
{
    public class CreateInternalTransactionCommand : CreateFinancialTransactionCommand, IRequest<InternalTransactionDto>
    {
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
    }

    public class CreateInternalTransactionCommandHandler : IRequestHandler<CreateInternalTransactionCommand, InternalTransactionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateInternalTransactionCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InternalTransactionDto> Handle(CreateInternalTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new InternalTransaction
            {
                TotalAmount = request.TotalAmount,
                DueDate = request.DueDate,
                Description = request.Description,
                SendingAccountId = request.SendingAccountId,
                ReceivingAccountId = request.ReceivingAccountId
            };

            _context.InternalTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<InternalTransactionDto>(entity);
        }
    }
}
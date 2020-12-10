using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Queries.GetInternalTransactionById
{
    public class GetInternalTransactionByIdQuery : IRequest<InternalTransactionDto>
    {
        public int Id { get; set; }
    }

    public class GetInternalTransactionByIdQueryHandler : IRequestHandler<GetInternalTransactionByIdQuery, InternalTransactionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetInternalTransactionByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InternalTransactionDto> Handle(GetInternalTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.InternalTransactions
                .Include(transaction => transaction.TransactionCategories)
                    .ThenInclude(transactionCategory => transactionCategory.FinancialCategory)
                .FirstOrDefaultAsync(transaction => transaction.Id == request.Id)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(InternalTransaction), request.Id);
            }

            return _mapper.Map<InternalTransactionDto>(entity);
        }
    }
}

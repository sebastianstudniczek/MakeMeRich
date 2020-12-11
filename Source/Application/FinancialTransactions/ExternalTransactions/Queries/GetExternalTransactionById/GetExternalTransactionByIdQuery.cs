using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities.FinancialTransactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application
{
    public class GetExternalTransactionByIdQuery : IRequest<ExternalTransactionDto>
    {
        public int Id { get; set; }
    }

    public class GetExternalTransactionByIdQueryHandler : IRequestHandler<GetExternalTransactionByIdQuery, ExternalTransactionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetExternalTransactionByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExternalTransactionDto> Handle(GetExternalTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExternalTransactions
                .Include(transaction => transaction.TransactionCategories)
                    .ThenInclude(transactionCategory => transactionCategory.FinancialCategory)
                .FirstOrDefaultAsync(transaction => transaction.Id == request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(ExternalTransaction), request.Id);
            }

            return _mapper.Map<ExternalTransactionDto>(entity);
        }
    }
}
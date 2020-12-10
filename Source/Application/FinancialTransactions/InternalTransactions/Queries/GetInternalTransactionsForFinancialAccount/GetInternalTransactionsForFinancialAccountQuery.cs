using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MakeMeRich.Application.Common.Dtos.FinancialTransactions;
using MakeMeRich.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Queries
{
    public class GetInternalTransactionsForFinancialAccountQuery : IRequest<List<InternalTransactionDto>>
    {
        public int FinancialAccountId { get; set; }
    }

    public class GetInternalTransactionsForFinancialAccountQueryHandler
        : IRequestHandler<GetInternalTransactionsForFinancialAccountQuery, List<InternalTransactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetInternalTransactionsForFinancialAccountQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<InternalTransactionDto>> Handle(GetInternalTransactionsForFinancialAccountQuery request, CancellationToken cancellationToken)
        {
            return _context.InternalTransactions
                .Where(transaction =>
                    transaction.ReceivingAccountId == request.FinancialAccountId ||
                    transaction.SendingAccountId == request.FinancialAccountId)
                .ProjectTo<InternalTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}

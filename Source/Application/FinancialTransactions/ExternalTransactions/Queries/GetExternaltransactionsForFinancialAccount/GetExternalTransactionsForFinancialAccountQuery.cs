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

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Queries.GetExternaltransactionsForFinancialAccountQuery
{
    public class GetExternalTransactionsForFinancialAccountQuery : IRequest<List<ExternalTransactionDto>>
    {
        public int FinancialAccountId { get; set; }
    }

    public class GetExternalTransactionsForFinancialAccountQueryHandler : IRequestHandler<GetExternalTransactionsForFinancialAccountQuery, List<ExternalTransactionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetExternalTransactionsForFinancialAccountQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<ExternalTransactionDto>> Handle(GetExternalTransactionsForFinancialAccountQuery request, CancellationToken cancellationToken)
        {
            return _context.ExternalTransactions
                .Where(transaction => transaction.FinancialAccountId == request.FinancialAccountId)
                .ProjectTo<ExternalTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}

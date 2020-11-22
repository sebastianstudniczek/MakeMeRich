using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MakeMeRich.Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountsQueryHandler : IRequestHandler<GetFinancialAccountsQuery, List<FinancialAccountDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinancialAccountsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<FinancialAccountDto>> Handle(GetFinancialAccountsQuery request, CancellationToken cancellationToken)
        {
            return _context.FinancialAccounts
                    .ProjectTo<FinancialAccountDto>(_mapper.ConfigurationProvider)
                    .OrderBy(account => account.Title)
                    .ToListAsync(cancellationToken);
        }
    }
}

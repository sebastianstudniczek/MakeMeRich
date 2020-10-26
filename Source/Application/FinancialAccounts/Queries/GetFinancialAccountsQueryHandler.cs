using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MakeMeRich.Application.Common.Interfaces;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountsQueryHandler : IRequestHandler<GetFinancialAccountsQuery, FinancialAccountsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinancialAccountsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<FinancialAccountsVm> Handle(GetFinancialAccountsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

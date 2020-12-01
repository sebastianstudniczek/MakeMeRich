using AutoMapper;

using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById
{
    public class GetFinancialAccountByIdQueryHandler
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetFinancialAccountByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialAccountDto> Handle(GetFinancialAccountByIdQuery query, CancellationToken none)
        {
            throw new NotImplementedException();
        }
    }
}

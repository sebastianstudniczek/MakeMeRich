﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;
using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById
{
    public class GetFinancialAccountByIdQuery : IRequest<FinancialAccountDto>
    {
        public int Id { get; set; }
    }
    public class GetFinancialAccountByIdQueryHandler : IRequestHandler<GetFinancialAccountByIdQuery, FinancialAccountDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinancialAccountByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialAccountDto> Handle(GetFinancialAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialAccounts
                .FindAsync(new object[] { request.Id }, cancellationToken)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(FinancialAccount), request.Id);
            }

            return _mapper.Map<FinancialAccountDto>(entity);
        }
    }
}

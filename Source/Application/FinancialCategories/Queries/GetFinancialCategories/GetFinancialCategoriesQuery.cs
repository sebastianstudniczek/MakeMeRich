using AutoMapper;
using AutoMapper.QueryableExtensions;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MakeMeRich.Application
{
    public class GetFinancialCategoriesQuery : IRequest<List<FinancialCategoryDto>>
    {

    }

    public class GetFinancialCategoriesQueryHandler : IRequestHandler<GetFinancialCategoriesQuery, List<FinancialCategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinancialCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FinancialCategoryDto>> Handle(GetFinancialCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.FinancialCategories
                .ProjectTo<FinancialCategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
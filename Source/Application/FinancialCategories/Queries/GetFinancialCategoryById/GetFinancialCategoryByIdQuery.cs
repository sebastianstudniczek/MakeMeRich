using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Exceptions;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;
using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Queries.GetFinancialCategoryById
{
    public class GetFinancialCategoryByIdQuery : IRequest<FinancialCategoryDto>
    {
        public int Id { get; set; }
    }

    public class GetFinancialCategoryByIdQueryHandler : IRequestHandler<GetFinancialCategoryByIdQuery, FinancialCategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFinancialCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialCategoryDto> Handle(GetFinancialCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.FinancialCategories
                .FindAsync(new object[] { request.Id }, cancellationToken)
                .ConfigureAwait(false);

            if (entity is null)
            {
                throw new NotFoundException(nameof(FinancialCategory), request.Id);
            }

            return _mapper.Map<FinancialCategoryDto>(entity);
        }
    }
}

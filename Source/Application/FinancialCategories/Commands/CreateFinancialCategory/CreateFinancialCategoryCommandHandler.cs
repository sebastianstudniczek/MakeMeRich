using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;

using MediatR;

namespace MakeMeRich.Application.FinancialCategories.Commands.CreateFinancialCategory
{
    public class CreateFinancialCategoryCommandHandler : IRequestHandler<CreateFinancialCategoryCommand, FinancialCategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFinancialCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialCategoryDto> Handle(CreateFinancialCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new FinancialCategory
            {
                Name = request.Name
            };

            _context.FinancialCategories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<FinancialCategoryDto>(entity);
        }
    }
}

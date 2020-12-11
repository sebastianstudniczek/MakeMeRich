using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MakeMeRich.Application.Common.Dtos;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Enums;
using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommand : IRequest<FinancialAccountDto>
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public FinancialAccountType Type { get; set; }
    }

    public class CreateFinancialAccountCommandHandler : IRequestHandler<CreateFinancialAccountCommand, FinancialAccountDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateFinancialAccountCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FinancialAccountDto> Handle(CreateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new FinancialAccount()
            {
                Title = request.Title,
                CurrentBalance = request.CurrentBalance,
                AccountType = request.Type
            };

            _context.FinancialAccounts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<FinancialAccountDto>(entity);        }
    }
}

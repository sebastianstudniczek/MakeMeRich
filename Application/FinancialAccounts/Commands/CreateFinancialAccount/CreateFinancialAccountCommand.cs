
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommand : IRequest<int>
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
    }

    public class CreateFinancialAccountCommandHandler : IRequestHandler<CreateFinancialAccountCommand, int>
    {
        public Task<int> Handle(CreateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

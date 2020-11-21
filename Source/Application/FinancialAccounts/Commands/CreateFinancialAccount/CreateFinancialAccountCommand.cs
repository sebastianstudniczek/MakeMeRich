using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommand : IRequest<int>
    {
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
    }
}

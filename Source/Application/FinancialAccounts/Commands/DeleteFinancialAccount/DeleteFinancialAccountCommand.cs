using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.DeleteFinancialAccount
{
    public class DeleteFinancialAccountCommand : IRequest
    {
        public int Id { get; set; }
    }
}

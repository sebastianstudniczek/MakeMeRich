using MakeMeRich.Domain.Enums;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Commands.UpdateFinancialAccount
{
    public class UpdateFinancialAccountCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double CurrentBalance { get; set; }
        public FinancialAccountType Type { get; set; }
    }
}

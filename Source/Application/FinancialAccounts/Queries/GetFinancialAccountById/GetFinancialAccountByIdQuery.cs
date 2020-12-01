using MakeMeRich.Application.Common.Dtos;
using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries.GetFinancialAccountById
{
    public class GetFinancialAccountByIdQuery : IRequest<FinancialAccountDto>
    {
        public int Id { get; set; }
    }
}

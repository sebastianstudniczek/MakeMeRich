using System.Collections.Generic;

using MakeMeRich.Application.FinancialAccounts.Queries.Dtos;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountsQuery : IRequest<List<FinancialAccountDto>>
    {

    }
}

using System.Collections.Generic;

using MakeMeRich.Application.Common.Dtos;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountsQuery : IRequest<List<FinancialAccountDto>>
    {

    }
}

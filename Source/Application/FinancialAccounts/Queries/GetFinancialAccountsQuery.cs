using System.Collections.Generic;
using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountsQuery : IRequest<List<FinancialAccountDto>>
    {

    }
}

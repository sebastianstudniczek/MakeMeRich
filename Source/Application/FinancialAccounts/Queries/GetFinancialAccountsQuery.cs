using System;
using System.Collections.Generic;
using System.Text;

using MakeMeRich.Application.Common.Interfaces;

using MediatR;

namespace MakeMeRich.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountsQuery : IRequest<FinancialAccountsVm>
    {
        
    }
}

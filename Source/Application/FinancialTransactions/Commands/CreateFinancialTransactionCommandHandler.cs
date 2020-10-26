using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.Commands
{
    public class CreateFinancialTransactionCommandHandler : IRequestHandler<CreateFinancialTransactionCommand, int>
    {
        public Task<int> Handle(CreateFinancialTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

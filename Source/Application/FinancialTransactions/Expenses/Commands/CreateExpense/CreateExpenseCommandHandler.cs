using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.Expenses.Commands.CreateExpense
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateExpenseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

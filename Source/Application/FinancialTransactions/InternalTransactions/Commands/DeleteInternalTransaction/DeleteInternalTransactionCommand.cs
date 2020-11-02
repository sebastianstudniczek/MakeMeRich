
using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.DeleteInternalTransaction
{
    public class DeleteInternalTransactionCommand : IRequest
    {
        public int Id { get; set; }
    }
}

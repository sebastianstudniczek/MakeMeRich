using MediatR;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.DeleteExternalTransaction
{
    public class DeleteExternalTransactionCommand : IRequest
    {
        public int Id { get; set; }
    }
}

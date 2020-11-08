using MakeMeRich.Application.FinancialTransactions.Common.Commands;

namespace MakeMeRich.Application.FinancialTransactions.InternalTransactions.Commands.UpdateInternalTransaction
{
    public class UpdateInternalTransactionCommand : UpdateFinancialTransactionCommand
    {
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
    }
}
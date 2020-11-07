using MakeMeRich.Application.FinancialTransactions.Common.Commands;
using MakeMeRich.Domain.Enums;

namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction
{
    public class CreateExternalTransactionCommand : CreateFinancialTransactionCommand
    {
        public string TransactionSideName { get; set; }
        public ExternalTransactionType Type { get; set; }
    }
}

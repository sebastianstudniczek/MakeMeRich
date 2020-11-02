using MakeMeRich.Application.FinancialTransactions.Common.Commands;

namespace MakeMeRich.Application
{
    public class CreateInternalTransactionCommand : CreateFinancialTransactionCommand
    {
        public int FinancialAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
    }
}
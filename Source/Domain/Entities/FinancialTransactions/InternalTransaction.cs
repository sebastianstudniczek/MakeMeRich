namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class InternalTransaction : FinancialTransaction
    {
        public int ReceivingAccountId { get; set; }
        public FinancialAccount ReceivingAccount { get; set; }
    }
}

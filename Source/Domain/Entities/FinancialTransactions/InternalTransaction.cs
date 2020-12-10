namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class InternalTransaction : FinancialTransaction
    {
        public int SendingAccountId { get; set; }
        public FinancialAccount SendingAccount { get; set; }
        public int ReceivingAccountId { get; set; }
        public FinancialAccount ReceivingAccount { get; set; }
    }
}

namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class InternalFinancialTransaction : FinancialTransaction
    {
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
    }
}

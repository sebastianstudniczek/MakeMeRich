namespace MakeMeRich.Domain.Entities.FinancialTransactions
{
    public class Transfer : FinancialTransaction
    {
        public int SendingAccountId { get; set; }
        public int ReceivingAccountId { get; set; }
    }
}

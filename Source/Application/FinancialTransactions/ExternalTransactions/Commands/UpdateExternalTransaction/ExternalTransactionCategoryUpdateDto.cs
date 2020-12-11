namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.UpdateExternalTransaction
{
    public class ExternalTransactionCategoryUpdateDto
    {
        public int FinancialCategoryId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}

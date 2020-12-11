namespace MakeMeRich.Application.FinancialTransactions.ExternalTransactions.Commands.CreateExternalTransaction
{
    public class ExternalTransactionCategoryCreateDto
    {
        public int FinancialCategoryId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}

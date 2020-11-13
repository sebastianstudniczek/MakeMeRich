namespace MakeMeRich.Domain.Entities.FinancialTransactionCategories
{
    public abstract class FinancialTransactionCategory
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int FinancialCategoryId { get; set; }
        public FinancialCategory FinancialCategory { get; set; }
    }
}

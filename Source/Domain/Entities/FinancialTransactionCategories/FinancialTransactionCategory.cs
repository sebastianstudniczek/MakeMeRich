namespace MakeMeRich.Domain.Entities.FinancialTransactionCategories
{
    public abstract class FinancialTransactionCategory : BaseEntity
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int FinancialCategoryId { get; set; }
        public FinancialCategory Category { get; set; }
    }
}

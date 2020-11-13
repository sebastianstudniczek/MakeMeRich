using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class InternalTransactionCategoryConfiguration : IEntityTypeConfiguration<InternalTransactionCategory>
{
    public void Configure(EntityTypeBuilder<InternalTransactionCategory> builder)
    {
        builder
            .HasOne(transactionCategory => transactionCategory.InternalTransaction)
            .WithMany(category => category.TransactionCategories)
            .HasForeignKey(transactionCategory => transactionCategory.InternalTransactionId);

        builder
            .HasOne(transactionCategory => transactionCategory.FinancialCategory)
            .WithMany(category => category.InternalTransactionCategories)
            .HasForeignKey(transactionCategory => transactionCategory.FinancialCategoryId);

        builder.Property(property => property.Amount)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(property => property.Description)
            .HasMaxLength(150);

        builder.HasKey(
                 key => new
                 {
                     key.InternalTransactionId,
                     key.FinancialCategoryId
                 });
    }
}


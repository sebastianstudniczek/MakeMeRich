using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class InternalTransactionCategoryConfiguration : IEntityTypeConfiguration<InternalTransactionCategory>
{
    public void Configure(EntityTypeBuilder<InternalTransactionCategory> builder)
    {
        builder.HasKey(
                 key => new
                 {
                     key.InternalTransactionId,
                     key.FinancialCategoryId
                 });

        builder.Property(property => property.Amount)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(property => property.Description)
            .HasMaxLength(150);
    }
}


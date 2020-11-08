using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations
{
    public class TransactionCategoryConfiguration : IEntityTypeConfiguration<FinancialTransactionCategory>
    {
        public void Configure(EntityTypeBuilder<FinancialTransactionCategory> builder)
        {
            builder.HasKey(
                key => new
                {
                    key.FinancialTransactionId,
                    key.FinancialCategoryId
                });

            builder.Property(property => property.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(property => property.Description)
                .HasMaxLength(150);
        }
    }
}

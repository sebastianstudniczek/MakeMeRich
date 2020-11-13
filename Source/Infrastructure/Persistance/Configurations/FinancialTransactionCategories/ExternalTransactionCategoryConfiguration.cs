
using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations.FinancialTransactionCategories
{
    public class ExternalTransactionCategoryConfiguration : IEntityTypeConfiguration<ExternalTransactionCategory>
    {
        public void Configure(EntityTypeBuilder<ExternalTransactionCategory> builder)
        {
            builder
                .HasOne(transactionCategory => transactionCategory.ExternalTransaction)
                .WithMany(category => category.TransactionCategories)
                .HasForeignKey(transactionCategory => transactionCategory.ExternalTransactionId);

            builder
                .HasOne(transactionCategory => transactionCategory.FinancialCategory)
                .WithMany(category => category.ExternalTransactionCategories)
                .HasForeignKey(transactionCategory => transactionCategory.FinancialCategoryId);

            builder.Property(property => property.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(property => property.Description)
                .HasMaxLength(150);

            builder.HasKey(
                key => new
                {
                    key.ExternalTransactionId,
                    key.FinancialCategoryId
                });
        }
    }
}

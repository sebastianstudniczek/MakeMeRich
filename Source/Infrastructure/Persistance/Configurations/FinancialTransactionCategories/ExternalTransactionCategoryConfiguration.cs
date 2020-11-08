
using MakeMeRich.Domain.Entities.FinancialTransactionCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations.FinancialTransactionCategories
{
    public class ExternalTransactionCategoryConfiguration : IEntityTypeConfiguration<ExternalTransactionCategory>
    {
        public void Configure(EntityTypeBuilder<ExternalTransactionCategory> builder)
        {
            builder.HasKey(
                key => new
                {
                    key.ExternalTransactionId,
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

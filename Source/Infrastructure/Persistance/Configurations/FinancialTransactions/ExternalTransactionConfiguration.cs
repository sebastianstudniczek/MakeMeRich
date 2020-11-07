using System;

using MakeMeRich.Domain.Entities.FinancialTransactions;
using MakeMeRich.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations.FinancialTransactions
{
    public class ExternalTransactionConfiguration : IEntityTypeConfiguration<ExternalTransaction>
    {
        public void Configure(EntityTypeBuilder<ExternalTransaction> builder)
        {
            builder.Property(property => property.TotalAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(property => property.DueDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(property => property.Description)
                .HasMaxLength(150);

            builder.Property(property => property.TransactionSideName)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            builder.Property(property => property.Type)
                .HasConversion(
                    value => value.ToString(),
                    value => (ExternalTransactionType)Enum.Parse(typeof(ExternalTransactionType), value))
                .IsRequired();

        }
    }
}

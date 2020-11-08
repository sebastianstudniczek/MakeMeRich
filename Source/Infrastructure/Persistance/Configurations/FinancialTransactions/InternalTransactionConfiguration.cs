using MakeMeRich.Domain.Entities.FinancialTransactions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations.FinancialTransactions
{
    public class InternalTransactionConfiguration : IEntityTypeConfiguration<InternalTransaction>
    {
        public void Configure(EntityTypeBuilder<InternalTransaction> builder)
        {
            builder.HasOne(transaction => transaction.FinancialAccount)
                .WithMany(account => account.InternalTransactions)
                .HasForeignKey(transaction => transaction.FinancialAccountId);

            builder.HasOne(transaction => transaction.ReceivingAccount)
                .WithMany(account => account.InternalTransactions)
                .HasForeignKey(transaction => transaction.ReceivingAccountId);

            builder.Property(property => property.TotalAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(property => property.DueDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(property => property.Description)
                .HasMaxLength(150);
        }
    }
}

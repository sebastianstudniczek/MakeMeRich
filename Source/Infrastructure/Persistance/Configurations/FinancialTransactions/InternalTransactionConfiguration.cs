using MakeMeRich.Domain.Entities.FinancialTransactions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations.FinancialTransactions
{
    public class InternalTransactionConfiguration : IEntityTypeConfiguration<InternalTransaction>
    {
        public void Configure(EntityTypeBuilder<InternalTransaction> builder)
        {
            builder.HasKey(transaction => transaction.Id);

            builder.HasOne(transaction => transaction.SendingAccount)
                .WithMany(account => account.SendedInternalTransactions)
                .HasForeignKey(transaction => transaction.SendingAccountId);

            builder.HasOne(transaction => transaction.ReceivingAccount)
                .WithMany(account => account.ReceivedInternalTransactions)
                .HasForeignKey(transaction => transaction.ReceivingAccountId);

            builder.Property(property => property.TotalAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(property => property.DueDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(property => property.Description)
                .HasMaxLength(150);

            builder.HasCheckConstraint("CHK_InternalTransactions_SendingAccountId", "SendingAccountId != ReceivingAccountId");
            builder.HasCheckConstraint("CHK_InternalTransactions_ReceivingAccountId", "ReceivingAccountId != SendingAccountId");
        }
    }
}

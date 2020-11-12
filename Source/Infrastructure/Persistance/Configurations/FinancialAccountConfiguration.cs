using System;

using MakeMeRich.Domain.Entities;
using MakeMeRich.Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations
{
    public class FinancialAccountConfiguration : IEntityTypeConfiguration<FinancialAccount>
    {
        public void Configure(EntityTypeBuilder<FinancialAccount> builder)
        {
            builder.Property(property => property.Title)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            builder.Property(property => property.CurrentBalance)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(prop => prop.AccountType)
                .HasConversion(
                    value => value.ToString(),
                    value => (FinancialAccountType)Enum.Parse(typeof(FinancialAccountType), value))
                .HasColumnType("varchar(30)")
                .IsRequired();
        }
    }
}

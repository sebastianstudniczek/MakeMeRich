using MakeMeRich.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MakeMeRich.Infrastructure.Persistance.Configurations
{
    public class FinancialCategoryConfiguration : IEntityTypeConfiguration<FinancialCategory>
    {
        public void Configure(EntityTypeBuilder<FinancialCategory> builder)
        {
            builder.Property(property => property.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();
        }
    }
}

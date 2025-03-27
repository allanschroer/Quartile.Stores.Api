using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quartile.Stores.Domain.Models;

namespace Quartile.Stores.Infra.DbMappings
{
    public class CompanyMapping : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(a => a.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(a => a.ModifiedDate)
                .HasColumnType("datetime")
                .ValueGeneratedOnUpdate();
        }
    }
}

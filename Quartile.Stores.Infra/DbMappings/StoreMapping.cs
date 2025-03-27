using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quartile.Stores.Domain.Models;

namespace Quartile.Stores.Infra.DbMappings
{
    public class StoreMapping : IEntityTypeConfiguration<StoreModel>
    {
        public void Configure(EntityTypeBuilder<StoreModel> builder)
        {
            builder.ToTable("Store");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(a => a.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Provider)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.IsActive)
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(a => a.ModifiedDate)
                .HasColumnType("datetime")
                .ValueGeneratedOnUpdate();

            builder.HasOne(a => a.Company)
                .WithMany(b => b.Stores)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.Name)
                .HasDatabaseName("IX_Store_Name");

            builder.HasIndex(a => a.CompanyId)
                .HasDatabaseName("IX_Store_CompanyId");
        }
    }
}

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

            builder.HasKey(x => x.Id);

            builder.Property(s => s.Id)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(s => s.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Address)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(s => s.City)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.State)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.ZipCode)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.IsActive)
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(s => s.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(s => s.ModifiedDate)
                .HasColumnType("datetime")
                .ValueGeneratedOnUpdate();

            builder.HasOne(s => s.Company)
                .WithMany(c => c.Stores)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.Name)
                .HasDatabaseName("IX_Store_Name");
        }
    }
}

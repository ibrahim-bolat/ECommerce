using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Concrete.EfCore.Mappings;

    public class AddressMap:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(address => address.Id);
            builder.Property(address => address.Id).ValueGeneratedOnAdd();
            builder.Property(address => address.AddressTitle).HasMaxLength(100).IsRequired();
            builder.Property(address => address.AddressType).IsRequired();
            builder.Property(address => address.Street).IsRequired();
            builder.Property(address => address.MainStreet).IsRequired();
            builder.Property(address => address.District).IsRequired();
            builder.Property(address => address.City).IsRequired();
            builder.Property(address => address.Country).IsRequired();
            builder.Property(address => address.BuildingNo).IsRequired();
            builder.Property(address => address.FlatNo).IsRequired();
            builder.HasOne(address => address.AppUser).WithMany(user => user.Addresses)
                .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.SetNull);

        }
    }

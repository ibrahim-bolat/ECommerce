using ECommerce.Entities.Concrete;
using ECommerce.Entities.Enums;
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
            builder.Property(address => address.AddressType)
                .HasConversion(
                    a=>a.ToString(),
                    a=>(AddressType)Enum.Parse(typeof(AddressType),a))
                .IsRequired();
            builder.Property(address => address.Street).IsRequired();
            builder.Property(address => address.MainStreet).IsRequired();
            builder.Property(address => address.District).IsRequired();
            builder.Property(address => address.City).IsRequired();
            builder.Property(address => address.Country).IsRequired();
            builder.Property(address => address.BuildingNo).IsRequired();
            builder.Property(address => address.FlatNo).IsRequired();
            builder.HasOne(address => address.AppUser).WithMany(user => user.Addresses)
                .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.SetNull);
            builder.HasData(new Address
            {
                Id = 1, 
                AddressTitle = "Evim",
                AddressType = AddressType.Home,
                Street = "Ateş",
                MainStreet = "Atılım",
                NeighborhoodOrVillage = "Naci Bekir",
                District = "Yenimahalle",
                City ="Ankara",
                Country = "Turkiye",
                RegionOrState = "İç Anadolu",
                BuildingNo = "40",
                FlatNo = "7",
                PostalCode = "06500",
                AddressDetails = "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye",
                UserId = 1
            },
            new Address
            {
                Id = 2, 
                AddressTitle = "İş",
                AddressType = AddressType.Work,
                Street = "Kütahya",
                MainStreet = "Eskişehir Yolu",
                NeighborhoodOrVillage = "Mustafa Kemal",
                District = "Çankaya",
                City ="Ankara",
                Country = "Turkiye",
                RegionOrState = "İç Anadolu",
                BuildingNo = "280",
                FlatNo = "7",
                PostalCode = "06100",
                AddressDetails = "Mustafa Kemal Mahallesi ,Eskişehir Yolu  Kütahya Sok. No:280/7 06500 Çankaya/Ankara/Türkiye",
                UserId = 1
            });

        }
    }

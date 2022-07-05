using ECommerce.Entities.Concrete;
using ECommerce.Shared.Entities.Concrete.Enums;
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
            builder.Property(address => address.Street).HasMaxLength(250);
            builder.Property(address => address.MainStreet).HasMaxLength(250);
            builder.Property(address => address.NeighborhoodOrVillage).HasMaxLength(250).IsRequired();
            builder.Property(address => address.District).HasMaxLength(250).IsRequired();
            builder.Property(address => address.City).HasMaxLength(250).IsRequired();
            builder.Property(address => address.Country).HasMaxLength(250).IsRequired();
            builder.Property(address => address.RegionOrState).HasMaxLength(250);
            builder.Property(address => address.BuildingNo).HasMaxLength(10);
            builder.Property(address => address.FlatNo).HasMaxLength(10);
            builder.Property(address => address.PostalCode).HasMaxLength(5);
            builder.Property(address => address.AddressDetails).HasMaxLength(500).IsRequired();
            builder.Property(address => address.Note).HasMaxLength(500);
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
                DefaultAddress = false,
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
                DefaultAddress = true,
                UserId = 1
            });

        }
    }

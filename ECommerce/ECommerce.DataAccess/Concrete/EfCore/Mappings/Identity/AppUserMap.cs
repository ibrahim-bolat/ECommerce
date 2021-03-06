using System;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Concrete.EfCore.Mappings.Identity;

    public class AppUserMap:IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(100).IsRequired();
            builder.Property(user => user.UserIdendityNo).HasMaxLength(11);
            builder.Property(user => user.GenderType)
                .HasConversion(
                    a => a.ToString(),
                    a => (GenderEnum)Enum.Parse(typeof(GenderEnum), a));
            builder.Property(user => user.Note).HasMaxLength(500);
            builder.Property(user => user.DateOfBirth).HasColumnType("date");
            builder.HasMany(user => user.Addresses).WithOne(address => address.AppUser)
                .HasForeignKey(address => address.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(u => u.Addresses).AutoInclude();
            builder.HasMany(user => user.UserImages).WithOne(userImage => userImage.AppUser)
                .HasForeignKey(userImage => userImage.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(u => u.UserImages).AutoInclude();


            var hasher = new PasswordHasher<AppUser>();
            builder.HasData(new AppUser
            {
                Id = 1,
                FirstName = "İbrahim",
                LastName = "Bolat",
                UserName = "bolat6606",
                PhoneNumber = "+90(532)575-79-66",
                NormalizedUserName = "BOLAT6606",
                Email = "bolat6606@hotmail.com",
                NormalizedEmail = "BOLAT6606@HOTMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Ankara.06"),
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                //SecurityStamp = GenerateSecurityStamp(), bu şekilde oluşturulabilir ama 
                //her migrationda yenisi oluşur o yüzden sabit değer verilmeli
            });
    }
    }

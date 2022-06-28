using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Concrete.EfCore.Mappings;

    public class UserImageMap:IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(userImage => userImage.Id);
            builder.Property(userImage => userImage.Id).ValueGeneratedOnAdd();
            builder.Property(userImage => userImage.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(userImage => userImage.ImagePath).IsRequired();
            builder.Property(userImage => userImage.Note).HasMaxLength(250);
            builder.HasOne(userImage => userImage.AppUser).WithMany(user => user.UserImages)
                .HasForeignKey(userImage => userImage.UserId).OnDelete(DeleteBehavior.SetNull);
            builder.HasData(new UserImage()
            {
                Id = 1, 
                ImageTitle = "ProfilResmi",
                ImagePath ="/admin/images/layout_img/g1.jpg",
                ImageAltText = "Profil",
                isProfil = true,
                UserId =1
            });
        }
    }

using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Concrete.EfCore.Mappings;

    public class MainCategoryMap:IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.HasKey(mainCategory => mainCategory.Id);
            builder.Property(mainCategory => mainCategory.Id).ValueGeneratedOnAdd();
            builder.Property(mainCategory => mainCategory.Name).HasMaxLength(250).IsRequired();
            builder.Property(mainCategory => mainCategory.Note).HasMaxLength(500);
            builder.HasMany(mainCategory => mainCategory.SubCategories).WithOne(subCategory => subCategory.MainCategory)
                .HasForeignKey(subCategory => subCategory.MainCategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new MainCategory()
            {
                Id = 1, 
                Name = "Araçlar"
            }, new MainCategory()
                {
                    Id = 2, 
                    Name = "Yedek Parçalar"
                });
        }
    }

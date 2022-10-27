using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.Concrete.EfCore.Mappings.Identity;


public class AppRoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(role => role.Note).HasMaxLength(500);
            builder.HasData(new AppRole
            {
                Id = 1,
                Name = RoleType.Admin.ToString(),
                NormalizedName = "ADMIN"
            });
    }
    }

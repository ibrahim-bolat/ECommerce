using ECommerce.DataAccess.Concrete.EfCore.Mappings;
using ECommerce.DataAccess.Concrete.EfCore.Mappings.Identity;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.DataAccess.Concrete.EfCore.Contexts;

    public class DataContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserImage> UserImages  { get; set; }


        public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppRoleMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new IdentityUserRoleMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new UserImageMap());
            
        }
    }

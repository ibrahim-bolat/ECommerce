using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete.EfCore.Repository;

public class EfUserRepository:EfGenericRepository<AppUser>,IUserRepository
{
    public EfUserRepository(DataContext context) : base(context)
    {
        
    }
}
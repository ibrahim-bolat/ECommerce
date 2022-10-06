using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.DataAccess.Concrete.EntityFramework;

namespace ECommerce.DataAccess.Concrete.EfCore.Repository;

public class EfUserImageRepository:EfGenericRepository<UserImage>,IUserImageRepository
{
    public EfUserImageRepository(DataContext context) : base(context)
    {
    }
}
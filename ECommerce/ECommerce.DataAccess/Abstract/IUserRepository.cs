using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.DataAccess.Abstract;

namespace ECommerce.DataAccess.Abstract;

public interface IUserRepository:IGenericRepository<AppUser>
{
    
}
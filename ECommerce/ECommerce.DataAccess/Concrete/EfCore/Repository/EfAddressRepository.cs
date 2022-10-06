using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.DataAccess.Concrete.EntityFramework;

namespace ECommerce.DataAccess.Concrete.EfCore.Repository;

public class EfAddressRepository:EfGenericRepository<Address>,IAddressRepository
{
    public EfAddressRepository(DataContext context) : base(context)
    {
        
    }
}
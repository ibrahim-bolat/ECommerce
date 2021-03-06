using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.DataAccess.Concrete.EfCore.Repository;

namespace ECommerce.DataAccess.Concrete;

public class UnitOfWork:IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IAddressRepository AddressRepository { get; }
    public IUserImageRepository UserImageRepository { get; }

    public UnitOfWork(DataContext dataContext,IAddressRepository addressRepository,IUserImageRepository userImageRepository)
    {
        _dataContext = dataContext;
        AddressRepository = addressRepository;
        UserImageRepository = userImageRepository;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _dataContext.DisposeAsync();
    }
    
    public async Task<int> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
}
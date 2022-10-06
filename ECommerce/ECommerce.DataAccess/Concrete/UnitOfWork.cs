using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;

namespace ECommerce.DataAccess.Concrete;

public class UnitOfWork:IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IAddressRepository AddressRepository { get; }
    public IUserImageRepository UserImageRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWork(DataContext dataContext,IAddressRepository addressRepository,IUserImageRepository userImageRepository,IUserRepository userRepository)
    {
        _dataContext = dataContext;
        AddressRepository = addressRepository;
        UserImageRepository = userImageRepository;
        UserRepository = userRepository;
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
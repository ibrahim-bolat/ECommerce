namespace ECommerce.DataAccess.Abstract;

public interface IUnitOfWork:IAsyncDisposable
{
    IAddressRepository AddressRepository { get; }
    IUserImageRepository UserImageRepository { get; }
    IUserRepository UserRepository { get; }
    Task<int> SaveAsync();
}
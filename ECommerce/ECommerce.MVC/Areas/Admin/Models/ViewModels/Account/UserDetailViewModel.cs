namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;

public class UserDetailViewModel
{
    public UserViewModel UserViewModel { get; set; }
    public List<UserAddressViewModel> UserAddressViewModels { get; set; }
}
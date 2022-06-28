using ECommerce.Entities.Enums;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account.UserViewModels;

public class AddressViewModel
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifiedTime { get; set; } 
    public bool IsActive { get; set; } 
    public bool IsDeleted { get; set; } 
    public string CreatedByName { get; set; } 
    public string ModifiedByName { get; set; }
    public  string Note { get; set; }
    public  string AddressTitle{ get; set; }
    public  AddressType AddressType { get; set; }
    public  string Street{ get; set; }
    public  string MainStreet{ get; set; }
    public  string NeighborhoodOrVillage{ get; set; }
    public  string District{ get; set; }
    public  string City{ get; set; }
    public  string Country{ get; set; }
    public  string RegionOrState{ get; set; }
    public  string BuildingNo{ get; set; }
    public  string FlatNo{ get; set; }
    public  string PostalCode{ get; set; }
    public  string AddressDetails{ get; set; }
}
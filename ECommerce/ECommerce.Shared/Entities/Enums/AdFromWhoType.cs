using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;

public enum AdFromWhoType
{
    [Description("Sahibinden")]
    ByOwner = 1,
    
    [Description("Galeriden")]
    FromTheGalery = 2,
        
    [Description("Yetkili Bayiden")]
    FromAuthorizedDealer = 3
}
using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;
public enum AddressType
    {
        [Description("Ev")]
        Home = 1,
        
        [Description("İş")]
        Work = 2,
        
        [Description("Diğer")]
        Other = 3,
    }

using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;

public enum GearType
{
    [Description("Düz")]
    Manuel = 1,
    
    [Description("Yarı Otomatik")]
    SemiAutomatic = 2,
    
    [Description("Otomatik")]
    Automatic = 3
}
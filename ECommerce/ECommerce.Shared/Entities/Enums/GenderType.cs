using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;

public enum GenderType
{
    [Description("Belirtilmemiş")]
    Unspecified,
    
    [Description("Erkek")]
    Male,
    
    [Description("Kadın")]
    Female,
}
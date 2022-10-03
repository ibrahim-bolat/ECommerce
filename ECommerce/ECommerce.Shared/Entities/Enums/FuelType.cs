using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;

public enum FuelType
{
    [Description("Benzin")]
    Gasoline = 1,
    
    [Description("Benzin ve LPG")]
    GasolineAndLpg = 2,
    
    [Description("Dizel")]
    Diesel = 3,
    
    [Description("Hibrit")]
    Hybrid = 4,
    
    [Description("Elektrik")]
    Electric = 5
}
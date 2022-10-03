using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;

public enum AdVehicleStatus
{
    [Description("İkinci El")]
    SecondHand = 1,
    
    [Description("İthal Sıfır")]
    ImportedFirstHand = 2,
    
    [Description("Sıfır")]
    FirstHand = 3
}
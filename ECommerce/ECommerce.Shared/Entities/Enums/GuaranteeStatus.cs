using System.ComponentModel;

namespace ECommerce.Shared.Entities.Enums;

public enum GuaranteeStatus
{
    [Description("Evet")]
    Yes = 1,
    
    [Description("Hayır")]
    No = 2
}
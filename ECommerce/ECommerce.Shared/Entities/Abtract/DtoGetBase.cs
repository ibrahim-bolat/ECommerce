
using ECommerce.Shared.Utilities.ComplexTypes;

namespace ECommerce.Shared.Entities.Abtract;
public abstract class BaseDto
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }

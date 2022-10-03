using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Enums;

namespace ECommerce.Shared.Entities.Abtract;
public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
        public virtual DateTime ModifiedTime { get; set; } = DateTime.Now;
        public virtual bool IsActive { get; set; } = false;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual string CreatedByName { get; set; } = RoleType.Admin.ToString();
        public virtual string ModifiedByName { get; set; } = RoleType.Admin.ToString();
        public virtual string Note { get; set; }
    }
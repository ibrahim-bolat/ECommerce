using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Shared.Entities.Concrete;
public class BaseEntity:IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
        public virtual DateTime ModifiedTime { get; set; } = DateTime.Now;
        public virtual bool IsActive { get; set; } = false;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual string CreatedByName { get; set; } = "Owner";
        public virtual string ModifiedByName { get; set; } = "Owner";
        public virtual string Note { get; set; }
    }
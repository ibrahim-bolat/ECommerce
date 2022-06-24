
namespace ECommerce.Shared.Entities.Abtract;

    public interface IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } 
        public DateTime ModifiedTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public string Note { get; set; }
    }

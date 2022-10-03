using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Entities.Concrete;
public class VehicleImage:BaseEntity,IEntity
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ImageAltText { get; set; }
        
        public int AdId { get; set; }
        public Ad Ad { get; set; }
    }

using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Entities.Concrete;
public class UserImage:BaseEntity,IEntity
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ImageAltText { get; set; }
        public bool Profil { get; set; }
        public int UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }

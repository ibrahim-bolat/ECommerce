using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Entities.Concrete;
public class UserImage:BaseEntity
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ImageAltText { get; set; }
        public bool isProfil { get; set; }
        public int UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }

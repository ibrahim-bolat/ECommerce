using ECommerce.Shared.Entities.Abtract;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Entities.Concrete.Identity.Entities;
public class AppRole:IdentityRole<int>,IIdentityEntity
    {
        public  DateTime CreatedTime { get; set; } = DateTime.Now;
        public  DateTime ModifiedTime { get; set; } = DateTime.Now;
        public  bool IsActive { get; set; } = true;
        public  bool IsDeleted { get; set; } = false;
        public  string CreatedByName { get; set; } = "Owner";
        public  string ModifiedByName { get; set; } = "Owner";
        public  string Note { get; set; }
    }
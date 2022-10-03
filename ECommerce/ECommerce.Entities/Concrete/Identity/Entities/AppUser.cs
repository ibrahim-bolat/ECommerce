using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Enums;
using Microsoft.AspNetCore.Identity;


namespace ECommerce.Entities.Concrete.Identity.Entities;
public class AppUser:IdentityUser<int>,IIdentityEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public GenderType GenderType { get; set; }
        public string UserIdendityNo { get; set; }
        
        public  DateTime? DateOfBirth{get; set;}
        public  DateTime CreatedTime { get; set; } = DateTime.Now;
        public  DateTime ModifiedTime { get; set; } = DateTime.Now;
        public  bool IsActive { get; set; } = true;
        public  bool IsDeleted { get; set; } = false;
        public  string CreatedByName { get; set; } = "Owner";
        public  string ModifiedByName { get; set; } = "Owner";
        public  string Note { get; set; }
        public  List<Address> Addresses{ get; set; }
        public  List<UserImage> UserImages{ get; set; }
    }
    
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Enums;

namespace ECommerce.Entities.Concrete;

    public class Address : BaseEntity,IEntity
    {
        public  string AddressTitle{ get; set; }
        public  AddressEnum AddressType { get; set; }
        public  string Street{ get; set; }
        public  string MainStreet{ get; set; }
        public  string NeighborhoodOrVillage{ get; set; }
        public  string District{ get; set; }
        public  string City{ get; set; }
        public  string Country{ get; set; }
        public  string RegionOrState{ get; set; }
        public  string BuildingNo{ get; set; }
        public  string FlatNo{ get; set; }
        public  string PostalCode{ get; set; }
        public  string AddressDetails{ get; set; }
        
        public bool DefaultAddress { get; set; }
        public int UserId{ get; set; }
        public virtual AppUser AppUser{ get; set; }
    
}

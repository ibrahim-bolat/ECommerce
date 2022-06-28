using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Entities.Enums;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Entities.Concrete;

    public class Address : BaseEntity
    {
        public  string AddressTitle{ get; set; }
        public  AddressType AddressType { get; set; }
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
        public int UserId{ get; set; }
        public virtual AppUser AppUser{ get; set; }
    
}

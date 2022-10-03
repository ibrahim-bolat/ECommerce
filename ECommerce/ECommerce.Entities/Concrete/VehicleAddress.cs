using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Entities.Concrete;

    public class VehicleAddress : BaseEntity,IEntity
    {
        public  string AddressTitle{ get; set; }
        public  string NeighborhoodOrVillage{ get; set; }
        public  string District{ get; set; }
        public  string City{ get; set; }
        public  string PostalCode{ get; set; }
        public  string AddressDetails{ get; set; }
        
        public Ad Ad { get; set; }
    }

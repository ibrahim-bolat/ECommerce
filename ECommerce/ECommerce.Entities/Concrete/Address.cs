﻿using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Enums;

namespace ECommerce.Entities.Concrete;

    public class Address : BaseEntity,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber{ get; set; }
        public  string AddressTitle{ get; set; }
        public  AddressType AddressType { get; set; }
        public  string NeighborhoodOrVillage{ get; set; }
        public  string District{ get; set; }
        public  string City{ get; set; }
        public  string PostalCode{ get; set; }
        public  string AddressDetails{ get; set; }
        public bool DefaultAddress { get; set; }
        public int UserId{ get; set; }
        public AppUser AppUser{ get; set; }
    
}

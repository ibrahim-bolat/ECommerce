using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Enums;

namespace ECommerce.Entities.Concrete;

    public class Model : BaseEntity,IEntity
    {
        public string Name { get; set; }   // Golf Gibi
        public string EngineType { get; set; } //1.4 TSI and 1.6 TDI gibi
        public EngineCapacityType EngineCapacity { get; set; } //1598 cc gibi
        public EnginePowerType EnginePower { get; set; } //115 Beygir gibi
        public string EquipmentVariant { get; set; } //Confortline gibi
        public int ModelYear { get; set; } //2005 gibi
        public FuelType FuelType { get; set; } //Dizel gibi
        public GearType GearType { get; set; } //Otomatik gibi
        public int Kilometer { get; set; } //200000 gibi
        public BodyType BodyType { get; set; } //Hatchback 5 kapı gibi
        public TractionType TractionType { get; set; } //Önden Çekişli gibi
        public ModelColourType ModelColour { get; set; } //Beyaz gibi
        public GuaranteeStatus GuaranteeStatus { get; set; } //Var Yok gibi
        public PlateNationalityType PlateNationality { get; set; } // Türkiye (TR) Plakalı gibi

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        
        public Ad Ad { get; set; }
    }


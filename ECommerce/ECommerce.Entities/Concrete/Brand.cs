using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Entities.Concrete;

    public class Brand : BaseEntity,IEntity
    {
        public string Name { get; set; }  //Volkswagen gibi
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<Model> Models { get; set; }
    }

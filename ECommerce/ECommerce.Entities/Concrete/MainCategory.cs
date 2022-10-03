using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Entities.Concrete;

    public class MainCategory : BaseEntity,IEntity
    {
        public string Name { get; set; }  //Araçlar ,, Yedek Parça Gibi
        public List<SubCategory> SubCategories { get; set; }
    }

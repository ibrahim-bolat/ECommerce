using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Business.Dtos.AddressDtos;

public class AddressSummaryDto:BaseDto,IDto
{
    public int Id { get; set; }
    
    [Display(Name = "Adres Başlığı")]
    public  string AddressTitle{ get; set; }
    
    [Display(Name = "Detaylı Adres")]
    public  string AddressDetails{ get; set; }
    
    [Display(Name = "Varsayılan Adres")]
    public bool DefaultAddress { get; set; }
}
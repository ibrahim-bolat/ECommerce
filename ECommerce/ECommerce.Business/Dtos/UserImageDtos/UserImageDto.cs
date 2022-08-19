using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
namespace ECommerce.Business.Dtos.UserImageDtos;

public class UserImageDto:BaseDto,IDto
{
    public int Id { get; set; }
    
    [Display(Name = "Not")]
    public string Note { get; set; }
    
    [Display(Name = "Resim Başlığı")]
    public string ImageTitle { get; set; }
    
    [Display(Name = "Resim Yolu")]
    public string ImagePath { get; set; }
    
    [Display(Name = "Resim Kısa Açıklmaa")]
    public string ImageAltText { get; set; }
    
    [Display(Name = "Profil Resmimi?")]
    public bool Profil { get; set; }
    
    public int UserId { get; set; }
}
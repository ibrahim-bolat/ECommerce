using System.ComponentModel.DataAnnotations;
using ECommerce.Business.ValidationRules.CustomValidation.UserImage;
using ECommerce.Shared.Entities.Abtract;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Business.Dtos.UserImageDtos;

public class UserImageAddDto:BaseDto,IDto
{
    public int Id { get; set; }
    
    [Display(Name = "Not")]
    public string Note { get; set; }
    
    [Display(Name = "Resim Başlığı")]
    public string ImageTitle { get; set; }
    
    
    [Display(Name = "Resim Kısa Açıklmaa")]
    public string ImageAltText { get; set; }
    
    [Display(Name = "Profil Resmimi?")]
    public bool Profil { get; set; }
    
    [Display(Name="Resim Yükle")]
    [DataType(DataType.Upload)]
    [ImageMaxFileSize((5* 1024 * 1024),197,150)]//en fazla 5 mb izin veriyoruz.
    [ImageAllowedExtensions(new string[] { ".jpg",".jpeg" , ".png" })]//sadece bu uzantılar geçerli
    public IFormFile ImageFile { get; set; }
    
    public int UserId { get; set; }
}
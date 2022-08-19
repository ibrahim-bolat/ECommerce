using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Business.ValidationRules.CustomValidation.UserImage;

public class ImageAllowedExtensionsAttribute:ValidationAttribute
{
    private readonly string[] _extensions;
    public ImageAllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }
    
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        
        return ValidationResult.Success;
    }
    
    public string GetErrorMessage()
    {
        return $"Bu resim uzantısına izin verilmiyor!";
    }

}
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace ECommerce.Business.ValidationRules.CustomValidation.UserImage;

public class ImageMaxFileSizeAttribute:ValidationAttribute
{
    private readonly int _maxFileSize;
    private readonly int _minWidth;
    private readonly int _minHeight;
    public ImageMaxFileSizeAttribute(int maxFileSize,int minWidth,int minHeight)
    {
        _maxFileSize = maxFileSize;
        _minWidth = minWidth;
        _minHeight = minHeight;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        int height, width;
        if (file != null)
        {
            using (var image = Image.Load(file.OpenReadStream()))
            {
                width = image.Width;
                height = image.Height;
            }
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMaxFileSizeMessage());
            }
            if (width < _minWidth && height < _minHeight)
            {
                return new ValidationResult(GetErrorMinDimensionMessage());
            }
        }
        return ValidationResult.Success;
    }
    public string GetErrorMaxFileSizeMessage()
    {
        return $"Maximum İzin Verilen Dosya Boyutu { _maxFileSize} bytedır.";
    }
    public string GetErrorMinDimensionMessage()
    {
        return $"Yüklenen Resmin Minumum Genişliği { _minWidth}, Minumum Yükseliği { _minHeight} olmalıdır.({ _minWidth}x{ _minHeight})";
    }

}
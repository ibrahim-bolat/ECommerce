using ECommerce.Business.Dtos.UserImageDtos;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation.UserImage;

public class UserImageDtoValidator:AbstractValidator<UserImageDto>
{
    public UserImageDtoValidator()
    {
        RuleFor(userImage => userImage.ImageTitle)
            .NotNull()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(userImage => userImage.ImagePath)
            .NotNull()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden resim başlığını boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");
        
        RuleFor(userImage => userImage.ImageAltText)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(userImage => userImage.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}
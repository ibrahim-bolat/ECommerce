using ECommerce.Business.Dtos.UserDtos;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation.Account;

public class EditPasswordDtoValidator:AbstractValidator<EditPasswordDto>
{
    public EditPasswordDtoValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotNull()
            .WithMessage("Lütfen yeni şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen yeni şifrenizi boş geçmeyiniz...");

        RuleFor(x => x.ReNewPassword)
            .NotNull()
            .WithMessage("Lütfen tekrar yeni şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen tekrar yeni şifrenizi boş geçmeyiniz...")
            .Equal(x => x.NewPassword)
            .WithMessage("Lütfen yeni şifre ile aynı giriniz...");
    }
}
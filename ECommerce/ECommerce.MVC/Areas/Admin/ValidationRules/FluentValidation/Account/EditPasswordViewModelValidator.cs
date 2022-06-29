using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using FluentValidation;

namespace ECommerce.MVC.Areas.Admin.ValidationRules.FluentValidation.Account;

public class EditPasswordViewModelValidator:AbstractValidator<EditPasswordViewModel>
{
    public EditPasswordViewModelValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotNull()
            .WithMessage("Lütfen eski şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen eski şifrenizi boş geçmeyiniz...");

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
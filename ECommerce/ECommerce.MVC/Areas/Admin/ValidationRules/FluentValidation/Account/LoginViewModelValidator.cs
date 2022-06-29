using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using FluentValidation;

namespace ECommerce.MVC.Areas.Admin.ValidationRules.FluentValidation.Account;

public class LoginViewModelValidator:AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Lütfen şifenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...");
    }
}
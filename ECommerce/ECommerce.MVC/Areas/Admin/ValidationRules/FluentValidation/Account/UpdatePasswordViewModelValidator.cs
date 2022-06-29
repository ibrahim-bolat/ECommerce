using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using FluentValidation;

namespace ECommerce.MVC.Areas.Admin.ValidationRules.FluentValidation.Account;

public class UpdatePasswordViewModelValidator:AbstractValidator<UpdatePasswordViewModel>
{
    public UpdatePasswordViewModelValidator()
    {
        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen şifrenizi boş geçmeyiniz...");
    }
}
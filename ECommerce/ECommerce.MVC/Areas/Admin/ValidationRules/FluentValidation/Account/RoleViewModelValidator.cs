using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using FluentValidation;

namespace ECommerce.MVC.Areas.Admin.ValidationRules.FluentValidation.Account;

public class RoleViewModelValidator:AbstractValidator<RoleViewModel>
{
    public RoleViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Lütden rolü boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden rolü boş geçmeyiniz....");
    }
}
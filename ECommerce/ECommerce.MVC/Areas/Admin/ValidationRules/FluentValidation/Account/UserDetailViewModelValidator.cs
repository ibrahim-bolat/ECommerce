using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using FluentValidation;

namespace ECommerce.MVC.Areas.Admin.ValidationRules.FluentValidation.Account;

public class UserDetailViewModelValidator:AbstractValidator<UserDetailViewModel>
{
    public UserDetailViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen adınızı boş geçmeyiniz...")
            .Length(3, 100)
            .WithMessage("En az 3 en fazla 100 karakter girebilirsiniz...");
        
        RuleFor(x => x.LastName)
            .NotNull()
            .WithMessage("Lütfen soyadınızı boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen soyadınızı boş geçmeyiniz...")
            .Length(3, 100)
            .WithMessage("En az 3 en fazla 100 karakter girebilirsiniz...");

        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .Length(4, 20)
            .WithMessage("Lütfen kullanıcı adını 4 ile 20 karakter arasında giriniz...");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .NotEmpty()
            .WithMessage("Lütfen emaili boş geçmeyiniz...")
            .EmailAddress()
            .WithMessage("Lütfen uygun formatta e-mail adresi giriniz.");
    }
}
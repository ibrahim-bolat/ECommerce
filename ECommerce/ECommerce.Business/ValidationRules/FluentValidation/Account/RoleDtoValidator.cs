using ECommerce.Business.Dtos.RoleDtos;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation.Account;

public class RoleDtoValidator:AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Lütden rolü boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden rolü boş geçmeyiniz....")
            .Must(IsLetter)
            .WithMessage("Lütfen sadece harflerden oluşan kelime giriniz.");;
    }
    private bool IsLetter(string name)
    {
        return name.All(Char.IsLetter); //hepsi harfmi diye kontrol ediliyor
    }
}
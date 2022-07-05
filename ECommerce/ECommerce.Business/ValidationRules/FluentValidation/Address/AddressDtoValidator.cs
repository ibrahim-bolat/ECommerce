using ECommerce.Business.Dtos.AddressDtos;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation.Address;

public class AddressDtoValidator:AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(address => address.AddressTitle)
            .NotNull()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres başlığını boş geçmeyiniz....")
            .MaximumLength(100)
            .WithMessage("En fazla 100 karakter girebilirsiniz...");

        RuleFor(address => address.AddressType)
            .NotNull()
            .WithMessage("Lütden adres tipini boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden adres tipini boş geçmeyiniz....");
        
        RuleFor(address => address.Street)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(address => address.MainStreet)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");

        RuleFor(address => address.NeighborhoodOrVillage)
            .NotNull()
            .WithMessage("Lütden mahalle yada köyü boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden mahalle yada köyü boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(address => address.District)
            .NotNull()
            .WithMessage("Lütden ilçeyi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ilçeyi boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(address => address.City)
            .NotNull()
            .WithMessage("Lütden ili boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ili boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(address => address.Country)
            .NotNull()
            .WithMessage("Lütden ülkeyi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden ülkeyi boş geçmeyiniz....")
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");
        
        RuleFor(address => address.RegionOrState)
            .MaximumLength(250)
            .WithMessage("En fazla 250 karakter girebilirsiniz...");        
        
        RuleFor(address => address.BuildingNo)
            .MaximumLength(10)
            .WithMessage("En fazla 10 karakter girebilirsiniz...");
        
        RuleFor(address => address.FlatNo)
            .MaximumLength(10)
            .WithMessage("En fazla 10 karakter girebilirsiniz...");
        
        RuleFor(address => address.PostalCode)
            .MaximumLength(5)
            .WithMessage("En fazla 5 karakter girebilirsiniz...");
        
        RuleFor(address => address.AddressDetails)
            .NotNull()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .NotEmpty()
            .WithMessage("Lütden detaylı adresi boş geçmeyiniz....")
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
        
        RuleFor(address => address.Note)
            .MaximumLength(500)
            .WithMessage("En fazla 500 karakter girebilirsiniz...");
    }
}
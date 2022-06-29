using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using FluentValidation;

namespace ECommerce.MVC.Areas.Admin.ValidationRules.FluentValidation.Account;

public class RoleAssignViewModelValidator:AbstractValidator<RoleAssignViewModel>
{
    public RoleAssignViewModelValidator()
    {
        
    }
}
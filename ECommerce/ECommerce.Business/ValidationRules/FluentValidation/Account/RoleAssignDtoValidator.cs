using ECommerce.Business.Dtos.RoleDtos;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation.Account;

public class RoleAssignDtoValidator:AbstractValidator<RoleAssignDto>
{
    public RoleAssignDtoValidator()
    {
        
    }
}
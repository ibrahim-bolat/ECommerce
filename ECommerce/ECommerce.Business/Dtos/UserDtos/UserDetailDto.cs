using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Business.Dtos.UserDtos;

public class UserDetailDto:BaseDto,IDto
{
    public UserDto UserDto { get; set; }
    public List<AddressSummaryDto> UserAddressSummaryDtos { get; set; }
}

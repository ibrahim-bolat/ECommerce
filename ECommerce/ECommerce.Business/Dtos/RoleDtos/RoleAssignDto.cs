using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Business.Dtos.RoleDtos;



public class RoleAssignDto:BaseDto,IDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasAssign { get; set; }
    }

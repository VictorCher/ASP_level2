using System.Collections.Generic;
using System.Security.Claims;

namespace WebStore.Domain.DTO.Identity
{
    public abstract class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}

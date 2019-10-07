using System.Security.Claims;

namespace WebStore.Domain.DTO.Identity
{
    public class ReplaceClaimDTO : UserDTO
    {
        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }
    }
}
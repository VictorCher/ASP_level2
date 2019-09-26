using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.DTO.Identity
{
    public class AddLoginDTO : UserDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}

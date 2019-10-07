using System;

namespace WebStore.Domain.DTO.Identity
{
    public class SetLockoutDTO : UserDTO
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}

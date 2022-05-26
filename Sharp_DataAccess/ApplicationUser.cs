using Microsoft.AspNetCore.Identity;

namespace Sharp_DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

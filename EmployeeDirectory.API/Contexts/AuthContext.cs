using Microsoft.AspNet.Identity.EntityFramework;

namespace EmployeeDirectory.API.Contexts
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}
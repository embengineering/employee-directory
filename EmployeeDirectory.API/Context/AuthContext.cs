using Microsoft.AspNet.Identity.EntityFramework;

namespace EmployeeDirectory.API.Context
{
    public class AuthContext: IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext")
        {
            
        }
    }
}
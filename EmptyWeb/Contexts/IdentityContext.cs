using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmptyWeb.Contexts
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext() : base("IdentityContext")
        {
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this));
        }

        public UserManager<IdentityUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
    }
}

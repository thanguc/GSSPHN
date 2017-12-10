using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using EmptyWeb.Contexts;
using Microsoft.AspNet.Identity.EntityFramework;
using EmptyWeb.Shared;

[assembly: OwinStartup(typeof(EmptyWeb.Startup))]

namespace EmptyWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/TaiKhoan/Login")
            });
            CreateRolesAndAccounts();
        }

        private void CreateRolesAndAccounts()
        {
            using (var identityContext = new IdentityContext())
            {
                if (!identityContext.RoleManager.RoleExists(PageEnums.UserRole.ADMIN))
                {
                    identityContext.RoleManager.Create(new IdentityRole(PageEnums.UserRole.ADMIN));

                    var superAdmin = new IdentityUser("admin")
                    {
                        Email = "thanguc.94@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "01679659990",
                        PhoneNumberConfirmed = true
                    };
                    identityContext.UserManager.Create(superAdmin, "140316");
                    identityContext.UserManager.AddToRole(superAdmin.Id, PageEnums.UserRole.ADMIN);

                    var admin = new IdentityUser("giasu24h")
                    {
                        Email = "hotro.giasusuphamhanoi@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "0981494418",
                        PhoneNumberConfirmed = true
                    };
                    identityContext.UserManager.Create(admin, "140316");
                    identityContext.UserManager.AddToRole(admin.Id, PageEnums.UserRole.ADMIN);

                    var anonymous = new IdentityUser("anonymous");
                    identityContext.UserManager.Create(anonymous, "140316");
                }

                if (!identityContext.RoleManager.RoleExists(PageEnums.UserRole.USER))
                {
                    identityContext.RoleManager.Create(new IdentityRole(PageEnums.UserRole.USER));
                }
            }
        }
    }
}

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
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // In Startup, creating default Admin Role and default Admin User
            using (var identityContext = new IdentityContext())
            {
                if (!identityContext.RoleManager.RoleExists(PageEnums.UserRole.ADMIN))
                {
                    identityContext.RoleManager.Create(new IdentityRole(PageEnums.UserRole.ADMIN));

                    var adminUser1 = new IdentityUser("admin")
                    {
                        Email = "thanguc.94@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "01679659990",
                        PhoneNumberConfirmed = true
                    };
                    identityContext.UserManager.Create(adminUser1, "140316");
                    identityContext.UserManager.AddToRole(adminUser1.Id, PageEnums.UserRole.ADMIN);

                    var adminUser2 = new IdentityUser("giasu24h")
                    {
                        Email = "hotro.giasusuphamhanoi@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "0981494418",
                        PhoneNumberConfirmed = true
                    };
                    identityContext.UserManager.Create(adminUser2, "140316");
                    identityContext.UserManager.AddToRole(adminUser2.Id, PageEnums.UserRole.ADMIN);
                }

                // creating Creating Manager role    
                //if (!roleManager.RoleExists("Manager"))
                //{
                //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                //    role.Name = "Manager";
                //    roleManager.Create(role);
                //}
            }
        }
    }
}

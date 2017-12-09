using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using EmptyWeb.Data;
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
            using (var roleStore = new RoleStore<IdentityRole>())
            using (var roleManager = new RoleManager<IdentityRole>(roleStore))
            using (var userStore = new UserStore<IdentityUser>())
            using (var userManager = new UserManager<IdentityUser>(userStore))
            {
                if (!roleManager.RoleExists(UserRole.ADMIN))
                {
                    roleManager.Create(new IdentityRole(UserRole.ADMIN));

                    var adminUser1 = new IdentityUser("admin")
                    {
                        Email = "thanguc.94@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "01679659990",
                        PhoneNumberConfirmed = true
                    };
                    userManager.Create(adminUser1, "140316");
                    userManager.AddToRole(adminUser1.Id, UserRole.ADMIN);

                    var adminUser2 = new IdentityUser("giasu24h")
                    {
                        Email = "hotro.giasusuphamhanoi@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "0981494418",
                        PhoneNumberConfirmed = true
                    };
                    userManager.Create(adminUser2, "140316");
                    userManager.AddToRole(adminUser2.Id, UserRole.ADMIN);
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

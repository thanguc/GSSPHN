using EmptyWeb.Shared;
using EmptyWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class TaiKhoanController : BaseController
    {
        // GET: TaiKhoan
        public ActionResult Login(string returnUrl)
        {
            if (PageHelper.IsAuthenticated())
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "TrangChu");
                }
            }
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //var userStore = new UserStore<IdentityUser>(IdentityContext);
            //var userManager = new UserManager<IdentityUser>(userStore);
            var user = IdentityContext.UserManager.Find(model.Username, model.Password);

            if (user != null)
            {
                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = IdentityContext.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return OK(new { returnUrl = model.ReturnUrl });
                }
                else
                {
                    return OK(new { returnUrl = "/TrangChu/Index" });
                }
            }
            else
            {
                return Error("Tài khoản không tồn tại hoặc mật khẩu không khớp!");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "TrangChu");
        }

        [Authorize(Roles = PageEnums.UserRole.ADMIN)]
        [HttpPost]
        public ActionResult Create(string username, string password)
        {
            //var userStore = new UserStore<IdentityUser>(IdentityContext);
            //var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = username };
            IdentityResult result = IdentityContext.UserManager.Create(user, password);

            if (result.Succeeded)
            {
                return OK();
            }
            else
            {
                return Error(result.Errors);
            }
        }
    }
}
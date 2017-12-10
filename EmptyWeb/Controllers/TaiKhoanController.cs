using EmptyWeb.Contexts;
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
    [TraceLog]
    public class TaiKhoanController : BaseController
    {
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
        public ActionResult Create(string Username, string Password)
        {
            var user = new IdentityUser() { UserName = Username };
            IdentityResult result = IdentityContext.UserManager.Create(user, Password);

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
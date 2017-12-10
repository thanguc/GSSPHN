using EmptyWeb.Contexts;
using EmptyWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmptyWeb.Shared
{
    public static class PageHelper
    {
        public static List<Muc> GetAllMuc()
        {
            using (var db = new EntityContext())
            {
                return db.Muc.Include(m => m.ChuyenMucs).ToList();
            }
        }

        public static bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static string CurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

    }
}
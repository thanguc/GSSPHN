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

        public static string Limit(string src, int limit)
        {
            if (string.IsNullOrEmpty(src))
            {
                return string.Empty;
            }
            return src.Length > limit ? src.Substring(0, limit) + "..." : src;
        }

        public static string Limit(int src, int limit)
        {
            return src > limit ? limit + "<sup>+</sup>" : src.ToString();
        }
    }
}
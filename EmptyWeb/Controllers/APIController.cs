using EmptyWeb.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class APIController : Controller
    {
        private readonly GSSPHNDbContext _db = new GSSPHNDbContext();

        public ActionResult Imgur()
        {
            var dictionary = new Dictionary<string, object>();
            Request.Form.CopyTo(dictionary);

            var content = new string[dictionary.Count];
            var index = 0;
            foreach(var item in dictionary)
            {
                content[index] = string.Format("{0}:{1}", item.Key, item.Value);
                index++;
            }

            _db.SystemLog.Add(new Models.SystemLog {
                Content = string.Join(",", content)
            });
            _db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
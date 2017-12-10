using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class CommonListController : BaseController
    {
        [HttpPost]
        public ActionResult GetListQueQuan()
        {
            var result = EntityContext.QueQuan.Select(q => q.Ten).ToArray();
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetListTrinhDo()
        {
            var result = EntityContext.TrinhDo.Select(q => q.TenTrinhDo).ToArray();
            return Json(result);
        }
    }
}
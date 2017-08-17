using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class CommonController : BaseController
    {
        [HttpPost]
        public ActionResult GetListQueQuan()
        {
            var result = BaseContext.QueQuan.Select(q => q.Ten).ToArray();
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetListTrinhDo()
        {
            var result = BaseContext.TrinhDo.Select(q => q.TenTrinhDo).ToArray();
            return Json(result);
        }
    }
}
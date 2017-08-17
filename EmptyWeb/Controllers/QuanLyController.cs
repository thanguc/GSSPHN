using EmptyWeb.Shared;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class QuanLyController : BaseController
    {

        // GET: QuanLy
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetReportData()
        {
            var wantToBeTutorCount = BaseContext.DangKyGiaSu.Count(d => d.TrangThai == TrangThaiDangKy.Submitted);
            var lookForTutorCount = 0;
            var feedbackCount = 0;
            var result = new
            {
                wantToBeTutorCount = wantToBeTutorCount,
                lookForTutorCount = lookForTutorCount,
                feedbackCount = feedbackCount
            };
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetAllDangKyLamGiaSu()
        {
            var submitting = BaseContext.DangKyGiaSu.Include(d => d.QueQuan).Include(d => d.TrinhDo).Where(d => d.TrangThai == TrangThaiDangKy.Submitted).OrderBy(d=>d.NgayTao);
            return PartialView("_WantToBeTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllLog()
        {
            return PartialView("_SystemLogs", BaseContext.SystemLog.ToList());
        }
    }
}
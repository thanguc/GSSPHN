using EmptyWeb.Data;
using EmptyWeb.Models;
using EmptyWeb.Services;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    public class DangKyLamGiaSuController : Controller
    {
        private GSSPHNDbContext _dbContext = new GSSPHNDbContext();

        // GET: DangKyLamGiaSu
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(DangKyGiaSu dangKy, HttpPostedFileBase fileAnhThe)
        {
            var imgurResult = await ImgurService.UploadImage(fileAnhThe.InputStream);
            if (imgurResult != null)
            {
                dangKy.AnhThe = imgurResult.Link;
                dangKy.ID = Guid.NewGuid().ToString();
                _dbContext.DangKyGiaSu.Add(dangKy);
                await _dbContext.SaveChangesAsync();
            }

            return View();
        }

    }
}
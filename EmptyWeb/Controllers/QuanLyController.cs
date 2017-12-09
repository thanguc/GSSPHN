using EmptyWeb.Models;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    [Authorize]
    public class QuanLyController : BaseController
    {

        // GET: QuanLy
        public ActionResult Index()
        {
            return View();
        }

        #region Report
        [HttpPost]
        public ActionResult GetReportData()
        {
            var wantToBeTutorCount = DBContext.DangKyGiaSu.Count(d => d.TrangThai == TrangThaiDangKy.Submitted);
            var lookForTutorCount = DBContext.TimGiaSu.Count(d => d.TrangThai == TrangThaiYeuCau.Submitted);
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
            var submitting = DBContext.DangKyGiaSu.Include(d => d.QueQuan).Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_WantToBeTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllYeuCauTimGiaSu()
        {
            var submitting = DBContext.TimGiaSu.Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_LookingForTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllLog()
        {
            return PartialView("_SystemLogs", DBContext.SystemLog.ToList());
        }
        #endregion

        #region Content

        public ActionResult Muc()
        {
            List<SelectListItem> listMuc = new List<SelectListItem>();
            listMuc.Add(new SelectListItem
            {
                Value = string.Empty,
                Text = "- Không có mục -"
            });
            listMuc.AddRange(DBContext.Muc.Select(m => new SelectListItem { Value = m.MucId.ToString(), Text = m.TieuDe }).ToList());
            ViewBag.Muc = listMuc;

            var result = DBContext.Muc.ToList();
            return PartialView("_Muc", result);
        }

        public ActionResult ChuyenMuc()
        {
            var result = DBContext.ChuyenMuc.Include(x => x.Muc).ToList();
            return PartialView("_ChuyenMuc", result);
        }

        public ActionResult ThemMuc(Muc model)
        {
            if (ModelState.IsValid)
            {
                model.SortNumber = DBContext.Muc.Count() + 1;
                DBContext.Muc.Add(model);
                DBContext.SaveChanges();
                return OK();
            }
            return Error(ModelState);
        }

        public ActionResult UpdateTieuDeMuc(Guid pk, string value)
        {
            var muc = DBContext.Muc.Find(pk);
            muc.TieuDe = value;
            if (DBContext.IsValid)
            {
                DBContext.SaveObject(muc);
                return OK();
            }
            else
            {
                return Error(DBContext);
            }
        }

        public ActionResult AddEditChuyenMuc(ChuyenMuc model)
        {
            ChuyenMuc cm = DBContext.ChuyenMuc.Find(model.ChuyenMucId);
            if (cm == null)
            {
                model.SortNumber = DBContext.ChuyenMuc.Count(c => c.MucId == model.MucId) + 1;
                DBContext.ChuyenMuc.Add(model);
                if (DBContext.IsValid)
                {
                    DBContext.SaveChanges();
                }
                else
                {
                    return Error(DBContext);
                }
            }
            else
            {
                cm.IsHidden = model.IsHidden;
                cm.Url = model.Url;
                cm.TieuDe = model.TieuDe;
                cm.NoiDung = model.NoiDung;
                if (DBContext.IsValid)
                {
                    DBContext.SaveObject(cm);
                }
                else
                {
                    return Error(DBContext);
                }
            }
            return OK();
        }

        public ActionResult GetSoLuongChuyenMuc(Guid id)
        {
            return Json(DBContext.Muc.Find(id).ChuyenMucs.Count);
        }

        public ActionResult ChiTietMuc(Guid id)
        {
            var result = DBContext.Muc.Find(id);
            return PartialView("_ChiTietMuc", result);
        }

        public ActionResult SwitchMucAnHien(Guid id, bool status)
        {
            var muc = DBContext.Muc.Find(id);
            muc.IsHidden = !status;
            DBContext.SaveObject(muc);
            return OK();
        }

        public ActionResult SwitchChuyenMucAnHien(Guid id, bool status)
        {
            var chuyenMuc = DBContext.ChuyenMuc.Find(id);
            chuyenMuc.IsHidden = !status;
            DBContext.SaveObject(chuyenMuc);
            return OK();
        }

        public ActionResult XoaMuc(Guid id)
        {
            var muc = DBContext.Muc.Find(id);
            var nextMucs = DBContext.Muc.Where(m => m.SortNumber > muc.SortNumber);
            foreach (var m in nextMucs)
            {
                m.SortNumber--;
            }
            DBContext.ChuyenMuc.RemoveRange(muc.ChuyenMucs);
            DBContext.DeleteObject(muc);
            return OK();
        }

        public ActionResult XoaChuyenMuc(Guid id)
        {
            var chuyenMuc = DBContext.ChuyenMuc.Find(id);
            var nextChuyenMucs = DBContext.ChuyenMuc.Where(m => m.SortNumber > chuyenMuc.SortNumber);
            foreach (var m in nextChuyenMucs)
            {
                m.SortNumber--;
            }
            if (DBContext.IsValid)
            {
                DBContext.DeleteObject(chuyenMuc);
            }
            else
            {
                return Error(DBContext);
            }
            return OK();
        }

        public ActionResult MoveMuc(Guid id, int value)
        {
            var muc = DBContext.Muc.Find(id);
            if (value == -1 && muc.SortNumber > 1)
            {
                var preMuc = DBContext.Muc.Single(c => c.SortNumber == muc.SortNumber - 1);
                muc.SortNumber--;
                preMuc.SortNumber++;
                if (DBContext.IsValid)
                {
                    DBContext.SaveObject(muc, preMuc);
                }
                else
                {
                    return Error(DBContext);
                }
            }
            else if (value == 1 && muc.SortNumber < DBContext.Muc.Count())
            {
                var nextMuc = DBContext.Muc.Single(c => c.SortNumber == muc.SortNumber + 1);
                muc.SortNumber++;
                nextMuc.SortNumber--;
                DBContext.SaveObject(muc, nextMuc);
            }
            return OK();
        }

        public ActionResult MoveChuyenMuc(Guid id, int value)
        {
            var chuyenMuc = DBContext.ChuyenMuc.Find(id);
            if (value == -1 && chuyenMuc.SortNumber > 1)
            {
                var preChuyenMuc = DBContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber - 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber--;
                preChuyenMuc.SortNumber++;
                DBContext.SaveObject(chuyenMuc, preChuyenMuc);
            }
            else if (value == 1 && chuyenMuc.SortNumber < DBContext.ChuyenMuc.Count(c => c.MucId == chuyenMuc.MucId))
            {
                var nextChuyenMuc = DBContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber + 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber++;
                nextChuyenMuc.SortNumber--;
                DBContext.SaveObject(chuyenMuc, nextChuyenMuc);
            }
            return OK();
        }

        public ActionResult GetChuyenMucModal(Guid? id)
        {
            return PartialView("_ModalThemChuyenMuc", id.HasValue ? DBContext.ChuyenMuc.Find(id) : null);
        }

        #endregion

        #region Account
        public ActionResult _TaiKhoan()
        {
            var accounts = DBContext.User;
            return PartialView("_TaiKhoan/_TaiKhoan", accounts.ToList());
        }
        #endregion

    }
}
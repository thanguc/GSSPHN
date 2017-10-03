using EmptyWeb.Models;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
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

        #region Report
        [HttpPost]
        public ActionResult GetReportData()
        {
            var wantToBeTutorCount = BaseContext.DangKyGiaSu.Count(d => d.TrangThai == TrangThaiDangKy.Submitted);
            var lookForTutorCount = BaseContext.TimGiaSu.Count(d => d.TrangThai == TrangThaiYeuCau.Submitted);
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
            var submitting = BaseContext.DangKyGiaSu.Include(d => d.QueQuan).Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_WantToBeTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllYeuCauTimGiaSu()
        {
            var submitting = BaseContext.TimGiaSu.Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_LookingForTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllLog()
        {
            return PartialView("_SystemLogs", BaseContext.SystemLog.ToList());
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
            listMuc.AddRange(BaseContext.Muc.Select(m => new SelectListItem { Value = m.MucId.ToString(), Text = m.TieuDe }).ToList());
            ViewBag.Muc = listMuc;

            var result = BaseContext.Muc.ToList();
            return PartialView("_Muc", result);
        }

        public ActionResult ChuyenMuc()
        {
            var result = BaseContext.ChuyenMuc.Include(x => x.Muc).ToList();
            return PartialView("_ChuyenMuc", result);
        }

        public ActionResult ThemMuc(Muc model)
        {
            BaseContext.Muc.Add(model);
            BaseContext.SaveChanges();
            return OK();
        }

        public ActionResult UpdateTieuDeMuc(Guid pk, string value)
        {
            var muc = BaseContext.Muc.Find(pk);
            muc.TieuDe = value;
            BaseContext.SaveObject(muc);
            return OK();
        }

        public ActionResult AddEditChuyenMuc(ChuyenMuc model)
        {
            ChuyenMuc cm = BaseContext.ChuyenMuc.Find(model.ChuyenMucId);
            if (cm == null)
            {
                model.SortNumber = BaseContext.ChuyenMuc.Count(c => c.MucId == model.MucId) + 1;
                BaseContext.ChuyenMuc.Add(model);
                BaseContext.SaveChanges();
            }
            else
            {
                cm.IsHidden = model.IsHidden;
                cm.Url = model.Url;
                cm.TieuDe = model.TieuDe;
                cm.NoiDung = model.NoiDung;
                BaseContext.SaveObject(cm);
            }
            return OK();
        }

        public ActionResult GetSoLuongChuyenMuc(Guid id)
        {
            return Json(BaseContext.Muc.Find(id).ChuyenMucs.Count);
        }

        public ActionResult ChiTietMuc(Guid id)
        {
            var result = BaseContext.Muc.Find(id);
            return PartialView("_ChiTietMuc", result);
        }

        public ActionResult SwitchMucAnHien(Guid id, bool status)
        {
            var muc = BaseContext.Muc.Find(id);
            muc.IsHidden = !status;
            BaseContext.SaveObject(muc);
            return OK();
        }

        public ActionResult SwitchChuyenMucAnHien(Guid id, bool status)
        {
            var chuyenMuc = BaseContext.ChuyenMuc.Find(id);
            chuyenMuc.IsHidden = !status;
            BaseContext.SaveObject(chuyenMuc);
            return OK();
        }

        public ActionResult XoaMuc(Guid id)
        {
            var muc = BaseContext.Muc.Find(id);
            BaseContext.ChuyenMuc.RemoveRange(muc.ChuyenMucs);
            BaseContext.DeleteObject(muc);
            return OK();
        }

        public ActionResult XoaChuyenMuc(Guid id)
        {
            var chuyenMuc = BaseContext.ChuyenMuc.Find(id);
            BaseContext.DeleteObject(chuyenMuc);
            return OK();
        }

        public ActionResult MoveMuc(Guid id, int value)
        {
            var muc = BaseContext.Muc.Find(id);
            if (value == -1 && muc.SortNumber > 1)
            {
                var preMuc = BaseContext.Muc.Single(c => c.SortNumber == muc.SortNumber - 1);
                muc.SortNumber--;
                preMuc.SortNumber++;
                BaseContext.SaveObject(muc, preMuc);
            }
            if (value == 1 && muc.SortNumber < BaseContext.Muc.Count())
            {
                var nextMuc = BaseContext.Muc.Single(c => c.SortNumber == muc.SortNumber + 1);
                muc.SortNumber++;
                nextMuc.SortNumber--;
                BaseContext.SaveObject(muc, nextMuc);
            }
            return OK();
        }

        public ActionResult MoveChuyenMuc(Guid id, int value)
        {
            var chuyenMuc = BaseContext.ChuyenMuc.Find(id);
            if (value == -1 && chuyenMuc.SortNumber > 1)
            {
                var preChuyenMuc = BaseContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber - 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber--;
                preChuyenMuc.SortNumber++;
                BaseContext.SaveObject(chuyenMuc, preChuyenMuc);
            }
            if (value == 1 && chuyenMuc.SortNumber < BaseContext.ChuyenMuc.Count(c => c.MucId == chuyenMuc.MucId))
            {
                var nextChuyenMuc = BaseContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber + 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber++;
                nextChuyenMuc.SortNumber--;
                BaseContext.SaveObject(chuyenMuc, nextChuyenMuc);
            }
            return OK();
        }

        public ActionResult GetChuyenMucModal(Guid? id)
        {
            return PartialView("_ModalThemChuyenMuc", id.HasValue ? BaseContext.ChuyenMuc.Find(id) : null);
        }

        #endregion
    }
}
using EmptyWeb.Contexts;
using EmptyWeb.Models;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EmptyWeb.Controllers
{
    [TraceLog]
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
            var wantToBeTutorCount = EntityContext.DangKyGiaSu.Count(d => d.TrangThai == PageEnums.TrangThaiDangKy.Submitted);
            var lookForTutorCount = EntityContext.TimGiaSu.Count(d => d.TrangThai == PageEnums.TrangThaiYeuCau.Submitted);
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
            var submitting = EntityContext.DangKyGiaSu.Include(d => d.QueQuan).Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_WantToBeTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllYeuCauTimGiaSu()
        {
            var submitting = EntityContext.TimGiaSu.Include(d => d.TrinhDo).OrderBy(d => d.NgayTao);
            return PartialView("_LookingForTutor", submitting.ToList());
        }

        [HttpPost]
        public ActionResult GetAllLog()
        {
            return PartialView("_SystemLogs", LogContext.Logs.ToList());
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
            listMuc.AddRange(EntityContext.Muc.Select(m => new SelectListItem { Value = m.MucId.ToString(), Text = m.TieuDe }).ToList());
            ViewBag.Muc = listMuc;

            var result = EntityContext.Muc.ToList();
            return PartialView("_Muc", result);
        }

        public ActionResult ChuyenMuc()
        {
            var result = EntityContext.ChuyenMuc.Include(x => x.Muc).ToList();
            return PartialView("_ChuyenMuc", result);
        }

        public ActionResult ThemMuc(Muc model)
        {
            if (ModelState.IsValid)
            {
                model.SortNumber = EntityContext.Muc.Count() + 1;
                EntityContext.Muc.Add(model);
                EntityContext.SaveChanges();
                return OK();
            }
            return Error(ModelState);
        }

        public ActionResult UpdateTieuDeMuc(Guid pk, string value)
        {
            var muc = EntityContext.Muc.Find(pk);
            muc.TieuDe = value;
            if (EntityContext.IsValid)
            {
                EntityContext.SaveObject(muc);
                return OK();
            }
            else
            {
                return Error(EntityContext);
            }
        }

        public ActionResult AddEditChuyenMuc(ChuyenMuc model)
        {
            ChuyenMuc cm = EntityContext.ChuyenMuc.Find(model.ChuyenMucId);
            if (cm == null)
            {
                model.SortNumber = EntityContext.ChuyenMuc.Count(c => c.MucId == model.MucId) + 1;
                EntityContext.ChuyenMuc.Add(model);
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveChanges();
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            else
            {
                cm.IsHidden = model.IsHidden;
                cm.Url = model.Url;
                cm.TieuDe = model.TieuDe;
                cm.NoiDung = model.NoiDung;
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveObject(cm);
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            return OK();
        }

        public ActionResult GetSoLuongChuyenMuc(Guid id)
        {
            return Json(EntityContext.Muc.Find(id).ChuyenMucs.Count);
        }

        public ActionResult ChiTietMuc(Guid id)
        {
            var result = EntityContext.Muc.Find(id);
            return PartialView("_ChiTietMuc", result);
        }

        public ActionResult SwitchMucAnHien(Guid id, bool status)
        {
            var muc = EntityContext.Muc.Find(id);
            muc.IsHidden = !status;
            EntityContext.SaveObject(muc);
            return OK();
        }

        public ActionResult SwitchChuyenMucAnHien(Guid id, bool status)
        {
            var chuyenMuc = EntityContext.ChuyenMuc.Find(id);
            chuyenMuc.IsHidden = !status;
            EntityContext.SaveObject(chuyenMuc);
            return OK();
        }

        public ActionResult XoaMuc(Guid id)
        {
            var muc = EntityContext.Muc.Find(id);
            var nextMucs = EntityContext.Muc.Where(m => m.SortNumber > muc.SortNumber);
            foreach (var m in nextMucs)
            {
                m.SortNumber--;
            }
            EntityContext.ChuyenMuc.RemoveRange(muc.ChuyenMucs);
            EntityContext.DeleteObject(muc);
            return OK();
        }

        public ActionResult XoaChuyenMuc(Guid id)
        {
            var chuyenMuc = EntityContext.ChuyenMuc.Find(id);
            var nextChuyenMucs = EntityContext.ChuyenMuc.Where(m => m.SortNumber > chuyenMuc.SortNumber);
            foreach (var m in nextChuyenMucs)
            {
                m.SortNumber--;
            }
            if (EntityContext.IsValid)
            {
                EntityContext.DeleteObject(chuyenMuc);
            }
            else
            {
                return Error(EntityContext);
            }
            return OK();
        }

        public ActionResult MoveMuc(Guid id, int value)
        {
            var muc = EntityContext.Muc.Find(id);
            if (value == -1 && muc.SortNumber > 1)
            {
                var preMuc = EntityContext.Muc.Single(c => c.SortNumber == muc.SortNumber - 1);
                muc.SortNumber--;
                preMuc.SortNumber++;
                if (EntityContext.IsValid)
                {
                    EntityContext.SaveObject(muc, preMuc);
                }
                else
                {
                    return Error(EntityContext);
                }
            }
            else if (value == 1 && muc.SortNumber < EntityContext.Muc.Count())
            {
                var nextMuc = EntityContext.Muc.Single(c => c.SortNumber == muc.SortNumber + 1);
                muc.SortNumber++;
                nextMuc.SortNumber--;
                EntityContext.SaveObject(muc, nextMuc);
            }
            return OK();
        }

        public ActionResult MoveChuyenMuc(Guid id, int value)
        {
            var chuyenMuc = EntityContext.ChuyenMuc.Find(id);
            if (value == -1 && chuyenMuc.SortNumber > 1)
            {
                var preChuyenMuc = EntityContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber - 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber--;
                preChuyenMuc.SortNumber++;
                EntityContext.SaveObject(chuyenMuc, preChuyenMuc);
            }
            else if (value == 1 && chuyenMuc.SortNumber < EntityContext.ChuyenMuc.Count(c => c.MucId == chuyenMuc.MucId))
            {
                var nextChuyenMuc = EntityContext.ChuyenMuc.Single(c => c.SortNumber == chuyenMuc.SortNumber + 1 && c.MucId == chuyenMuc.MucId);
                chuyenMuc.SortNumber++;
                nextChuyenMuc.SortNumber--;
                EntityContext.SaveObject(chuyenMuc, nextChuyenMuc);
            }
            return OK();
        }

        public ActionResult GetChuyenMucModal(Guid? id)
        {
            return PartialView("_ModalThemChuyenMuc", id.HasValue ? EntityContext.ChuyenMuc.Find(id) : null);
        }

        #endregion

        #region Account
        public ActionResult _TaiKhoan()
        {
            var accounts = EntityContext.User;
            return PartialView("_TaiKhoan/_TaiKhoan", accounts.ToList());
        }
        #endregion

    }
}
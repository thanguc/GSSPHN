using System;
using System.Web.Mvc;

namespace EmptyWeb.Models
{
    public class ChuyenMuc
    {
        public Guid ChuyenMucId { get; set; } = Guid.NewGuid();
        public string TieuDe { get; set; } = string.Empty;
        [AllowHtml]
        public string NoiDung { get; set; } = string.Empty;
        public bool IsHidden { get; set; } = false;
        public string Url { get; set; } = string.Empty;
        public int? SortNumber { get; set; }
        public Guid? MucId { get; set; }

        public virtual Muc Muc { get; set; }
    }
}

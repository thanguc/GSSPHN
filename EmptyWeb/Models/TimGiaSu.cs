using EmptyWeb.Shared;
using System;

namespace EmptyWeb.Models
{
    public class TimGiaSu
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string YeuCau { get; set; }

        public PageEnums.GioiTinh? GioiTinh { get; set; }
        public int? TrinhDoId { get; set; }
        public string TrinhDoKhac { get; set; }
        public string DonVi { get; set; }
        public string ChuyenNganh { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public PageEnums.TrangThaiYeuCau TrangThai { get; set; }

        public virtual TrinhDo TrinhDo { get; set; }
    }

}
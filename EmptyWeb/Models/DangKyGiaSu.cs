using EmptyWeb.Shared;
using System;

namespace EmptyWeb.Models
{
    public class DangKyGiaSu
    {
        public string ID { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public PageEnums.GioiTinh? GioiTinh { get; set; }
        public int? QueQuanID { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string AnhThe { get; set; }
        public int? TrinhDoID { get; set; }
        public string TrinhDoKhac { get; set; }
        public string DonVi { get; set; }
        public string ChuyenNganh { get; set; }
        public string GioiThieu { get; set; }
        public PageEnums.TrangThaiDangKy TrangThai { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual QueQuan QueQuan { get; set; }
        public virtual TrinhDo TrinhDo { get; set; }
    }

}
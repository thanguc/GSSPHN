using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmptyWeb.Models
{
    public class DangKyGiaSu
    {
        public string ID { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public int QueQuan { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string AnhThe { get; set; }
        public int TrinhDo { get; set; }
        public string TrinhDoKhac { get; set; }
        public string DonVi { get; set; }
        public string ChuyenNganh { get; set; }
        public string GioiThieu { get; set; }
    }
}
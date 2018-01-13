using EmptyWeb.Contexts;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmptyWeb.Models
{
    public class DangKyGiaSu : IValidatableObject
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public PageEnums.GioiTinh? GioiTinh { get; set; }
        public int? QueQuanID { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        public string AnhThe { get; set; }
        public int? TrinhDoID { get; set; }
        public string TrinhDoKhac { get; set; }
        public string DonVi { get; set; }
        public string ChuyenNganh { get; set; }
        public string GioiThieu { get; set; }
        public PageEnums.TrangThaiDangKy TrangThai { get; set; } = PageEnums.TrangThaiDangKy.Submitted;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        public virtual QueQuan QueQuan { get; set; }
        public virtual TrinhDo TrinhDo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new EntityContext())
            {
                if (string.IsNullOrEmpty(this.HoTen))
                    yield return new ValidationResult("Họ tên không được để trống", new[] { "HoTen" });
                if (string.IsNullOrEmpty(this.NgaySinh))
                    yield return new ValidationResult("Ngày sinh không được để trống", new[] { "NgaySinh" });
                if (!this.GioiTinh.HasValue)
                    yield return new ValidationResult("Giới tính không được để trống", new[] { "GioiTinh" });
                if (string.IsNullOrEmpty(this.DiaChi))
                    yield return new ValidationResult("Địa chỉ không được để trống", new[] { "DiaChi" });
                if (string.IsNullOrEmpty(this.SDT))
                    yield return new ValidationResult("Số điện thoại không được để trống", new[] { "SDT" });
                if (string.IsNullOrEmpty(this.Email))
                    yield return new ValidationResult("Email không được để trống", new[] { "Email" });
                if (!this.TrinhDoID.HasValue)
                    yield return new ValidationResult("Trình độ không được để trống", new[] { "TrinhDoID" });
                if (this.TrinhDoID.HasValue && db.TrinhDo.Find(this.TrinhDoID).TenTrinhDo == "Khác" && string.IsNullOrEmpty(this.TrinhDoKhac))
                    yield return new ValidationResult("Thông tin trình độ khác không được bỏ trống", new[] { "TrinhDoKhac" });
                if (string.IsNullOrEmpty(this.DonVi))
                    yield return new ValidationResult("Đơn vị học tập/làm việc không được để trống", new[] { "DonVi" });
            }
        }
    }

}
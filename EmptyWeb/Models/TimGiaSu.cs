using EmptyWeb.Contexts;
using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmptyWeb.Models
{
    public class TimGiaSu : IValidatableObject
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
        public PageEnums.TrangThaiYeuCau TrangThai { get; set; } = PageEnums.TrangThaiYeuCau.Submitted;

        public virtual TrinhDo TrinhDo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new EntityContext())
            {
                if (string.IsNullOrEmpty(this.HoTen))
                    yield return new ValidationResult("Họ tên không được để trống", new[] { "HoTen" });
                if (string.IsNullOrEmpty(this.SDT))
                    yield return new ValidationResult("Số điện thoại không được để trống", new[] { "SDT" });
            }
        }
    }

}
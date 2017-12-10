namespace EmptyWeb.Shared
{
    public static class PageEnums
    {
        public enum AlertMessage
        {
            DangKyLamGiaSuThanhCong,
            DangKyTimGiaSuThanhCong,
            DangKyLamGiaSuThatBai,
            DangKyTimGiaSuThatBai
        }

        public enum TrangThaiDangKy
        {
            Submitted,
            Approved,
            Rejected
        }

        public enum TrangThaiYeuCau
        {
            Submitted,
            NotAssigned,
            Assigned,
            Cancelled
        }

        public enum GioiTinh
        {
            Male, Female
        }

        public class UserRole
        {
            public const string ADMIN = "Admin";
        }

        public static class Parser
        {
            public static string TrangThaiDangKy(TrangThaiDangKy status)
            {
                switch (status)
                {
                    case PageEnums.TrangThaiDangKy.Approved: return "<label class='label label-success'>Đã duyệt</label>";
                    case PageEnums.TrangThaiDangKy.Submitted: return "<label class='label label-warning'>Chưa duyệt</label>";
                    case PageEnums.TrangThaiDangKy.Rejected: return "<label class='label label-danger'>Đã bỏ</label>";
                    default: return string.Empty;
                }
            }

            public static string GioiTinh(int i)
            {
                switch (i)
                {
                    case 0: return "Nam";
                    case 1: return "Nữ";
                    default: return string.Empty;
                }
            }
        }
    }

}

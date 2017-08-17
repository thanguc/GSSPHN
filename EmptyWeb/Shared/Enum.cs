namespace EmptyWeb.Shared
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

    public static class Parser
    {
        public static string TrinhDo(int i)
        {
            switch (i)
            {
                case 0: return "Sinh Viên";
                case 1: return "Giáo Viên";
                case 2: return "Giảng Viên";
                case 3: return "Trợ Giảng";
                case 4: return "Khác";
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

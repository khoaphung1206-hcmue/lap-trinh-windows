namespace StudentReportDemo.Models
{
    public class SinhVien
    {
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string MaLop { get; set; } = "";
        public string TenLop { get; set; } = "";
        public double Diem { get; set; }

        /// <summary>
        /// Chi doc, tinh tu Diem nen khong bao gio bi lech so voi diem.
        /// </summary>
        public string KetQua => Diem >= 5 ? "Đạt" : "Không đạt";
    }
}

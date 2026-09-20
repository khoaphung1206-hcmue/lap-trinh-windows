using System;

namespace Lab03_QuanLySinhVienOOP
{
    // Class con: SinhVien ke thua tu Nguoi
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        private double diemTrungBinh;
        // Property kiem tra du lieu: chi nhan gia tri tu 0 den 10
        public double DiemTrungBinh
        {
            get => diemTrungBinh;
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(value), "Diem trung binh phai tu 0 den 10.");
                diemTrungBinh = value;
            }
        }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh; // se duoc kiem tra thong qua property
        }

        // Xep loai dua tren diem trung binh
        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.0) return "Gioi";
            if (DiemTrungBinh >= 6.5) return "Kha";
            if (DiemTrungBinh >= 5.0) return "Trung binh";
            return "Yeu";
        }

        // Override phuong thuc cua lop cha de hien thi day du thong tin sinh vien
        public override string LayThongTin()
        {
            return $"{MaSinhVien,-8}{HoTen,-25}{MaLop,-10}{DiemTrungBinh,-8:0.0}{XepLoai(),-12}";
        }
    }
}

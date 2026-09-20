using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    // Class quan ly danh sach sinh vien, Program khong duoc xu ly truc tiep List
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();

        // Them sinh vien, tra ve false neu ma da ton tai
        public bool Them(SinhVien sv)
        {
            if (danhSachSinhVien.Any(x => x.MaSinhVien.Equals(sv.MaSinhVien, StringComparison.OrdinalIgnoreCase)))
                return false;

            danhSachSinhVien.Add(sv);
            return true;
        }

        // Sua diem trung binh theo ma sinh vien, tra ve false neu khong tim thay
        public bool Sua(string maSinhVien, double diemMoi)
        {
            var sv = TimTheoMa(maSinhVien);
            if (sv == null) return false;

            sv.DiemTrungBinh = diemMoi;
            return true;
        }

        // Xoa sinh vien theo ma, tra ve false neu khong tim thay
        public bool Xoa(string maSinhVien)
        {
            var sv = TimTheoMa(maSinhVien);
            if (sv == null) return false;

            danhSachSinhVien.Remove(sv);
            return true;
        }

        // Tim theo ma (dung LINQ)
        public SinhVien TimTheoMa(string maSinhVien)
        {
            return danhSachSinhVien
                .FirstOrDefault(x => x.MaSinhVien.Equals(maSinhVien, StringComparison.OrdinalIgnoreCase));
        }

        // Tim theo ten, tra ve danh sach cac sinh vien co ten chua tu khoa (dung LINQ)
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return danhSachSinhVien
                .Where(x => x.HoTen.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        // Sap xep theo diem giam dan (dung LINQ)
        public List<SinhVien> SapXepTheoDiem()
        {
            return danhSachSinhVien
                .OrderByDescending(x => x.DiemTrungBinh)
                .ToList();
        }

        // Loc sinh vien dat (diem >= 5), dung LINQ
        public List<SinhVien> LocSinhVienDat()
        {
            return danhSachSinhVien
                .Where(x => x.DiemTrungBinh >= 5.0)
                .ToList();
        }

        // Lay toan bo danh sach
        public List<SinhVien> LayDanhSach()
        {
            return danhSachSinhVien;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Lab03_QuanLySinhVienOOP
{
    public class Program
    {
        private static QuanLySinhVien quanLy = new QuanLySinhVien();

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool tiepTuc = true;

            while (tiepTuc)
            {
                HienThiMenu();
                string luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1": ThemSinhVien(); break;
                    case "2": XuatDanhSach(quanLy.LayDanhSach()); break;
                    case "3": TimTheoMa(); break;
                    case "4": TimTheoTen(); break;
                    case "5": SuaDiem(); break;
                    case "6": XoaSinhVien(); break;
                    case "7": XuatDanhSach(quanLy.SapXepTheoDiem()); break;
                    case "8": XuatDanhSach(quanLy.LocSinhVienDat()); break;
                    case "0":
                        tiepTuc = false;
                        Console.WriteLine("Tam biet!");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le, vui long chon lai.");
                        break;
                }

                if (tiepTuc)
                {
                    Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void HienThiMenu()
        {
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon chuc nang: ");
        }

        // ===== Cac ham nhap lieu, tach rieng de Main khong xu ly truc tiep =====

        private static string NhapChuoi(string thongBao)
        {
            string ketQua;
            do
            {
                Console.Write(thongBao);
                ketQua = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ketQua))
                    Console.WriteLine("Gia tri khong duoc de trong, vui long nhap lai.");
            } while (string.IsNullOrWhiteSpace(ketQua));

            return ketQua.Trim();
        }

        private static DateTime NhapNgay(string thongBao)
        {
            DateTime ketQua;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out ketQua))
                    return ketQua;

                Console.WriteLine("Ngay sinh khong hop le (dinh dang vi du: 15/03/2004). Vui long nhap lai.");
            }
        }

        private static double NhapDiem(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();

                if (!double.TryParse(input, out double diem))
                {
                    Console.WriteLine("Diem khong hop le, vui long nhap so.");
                    continue;
                }

                if (diem < 0 || diem > 10)
                {
                    Console.WriteLine("Diem khong hop le, phai tu 0 den 10. Vui long nhap lai.");
                    continue;
                }

                return diem;
            }
        }

        // ===== Cac chuc nang chinh, goi xuong QuanLySinhVien =====

        private static void ThemSinhVien()
        {
            Console.WriteLine("--- Them sinh vien ---");
            string ma = NhapChuoi("Nhap ma sinh vien: ");
            string hoTen = NhapChuoi("Nhap ho ten: ");
            DateTime ngaySinh = NhapNgay("Nhap ngay sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoi("Nhap ma lop: ");
            double diem = NhapDiem("Nhap diem trung binh (0-10): ");

            try
            {
                SinhVien sv = new SinhVien(ma, hoTen, ngaySinh, maLop, diem);
                bool thanhCong = quanLy.Them(sv);

                if (thanhCong)
                    Console.WriteLine($"Them thanh cong. Xep loai: {sv.XepLoai()}");
                else
                    Console.WriteLine($"Ma sinh vien '{ma}' da ton tai.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }

        private static void XuatDanhSach(List<SinhVien> danhSach)
        {
            Console.WriteLine("--- Danh sach sinh vien ---");
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sach rong.");
                return;
            }

            Console.WriteLine($"{"Ma",-8}{"Ho ten",-25}{"Lop",-10}{"Diem",-8}{"Xep loai",-12}");
            foreach (var sv in danhSach)
            {
                Console.WriteLine(sv.LayThongTin());
            }
        }

        private static void TimTheoMa()
        {
            Console.WriteLine("--- Tim theo ma ---");
            string ma = NhapChuoi("Nhap ma sinh vien can tim: ");
            var sv = quanLy.TimTheoMa(ma);

            if (sv == null)
                Console.WriteLine("Khong tim thay sinh vien co ma nay.");
            else
                XuatDanhSach(new List<SinhVien> { sv });
        }

        private static void TimTheoTen()
        {
            Console.WriteLine("--- Tim theo ten ---");
            string tuKhoa = NhapChuoi("Nhap tu khoa ten can tim: ");
            var ketQua = quanLy.TimTheoTen(tuKhoa);

            if (ketQua.Count == 0)
                Console.WriteLine("Khong tim thay sinh vien nao phu hop.");
            else
                XuatDanhSach(ketQua);
        }

        private static void SuaDiem()
        {
            Console.WriteLine("--- Sua diem trung binh ---");
            string ma = NhapChuoi("Nhap ma sinh vien can sua: ");

            if (quanLy.TimTheoMa(ma) == null)
            {
                Console.WriteLine("Khong tim thay sinh vien co ma nay.");
                return;
            }

            double diemMoi = NhapDiem("Nhap diem trung binh moi (0-10): ");
            bool thanhCong = quanLy.Sua(ma, diemMoi);
            Console.WriteLine(thanhCong ? "Cap nhat diem thanh cong." : "Cap nhat that bai.");
        }

        private static void XoaSinhVien()
        {
            Console.WriteLine("--- Xoa sinh vien ---");
            string ma = NhapChuoi("Nhap ma sinh vien can xoa: ");
            bool thanhCong = quanLy.Xoa(ma);

            Console.WriteLine(thanhCong ? "Xoa thanh cong." : "Khong tim thay sinh vien co ma nay.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab04_ProductManager
{
    // ==========================================
    // 1. INTERFACE VA CUSTOM EXCEPTIONS
    // ==========================================

    public interface IEntity
    {
        // Thuộc tính Id bắt buộc phải giữ nguyên để làm key chung[cite: 1]
        string Id { get; }
    }

    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string thongBaoLoi) : base(thongBaoLoi) { }
    }

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string thongBaoLoi) : base(thongBaoLoi) { }
    }

    // ==========================================
    // 2. MODEL (LOP DOI TUONG)
    // ==========================================

    public class Product : IEntity
    {
        public string Id { get; set; } // Tuong duong MaSP
        public string TenSP { get; set; }

        private decimal donGia; // bien tieng Viet khong dau
        public decimal Price
        {
            get => donGia;
            set
            {
                if (value < 0) throw new ArgumentException("Don gia khong duoc am.");
                donGia = value;
            }
        }

        private int soLuong; // bien tieng Viet khong dau
        public int Quantity
        {
            get => soLuong;
            set
            {
                if (value < 0) throw new ArgumentException("So luong khong duoc am.");
                soLuong = value;
            }
        }

        public override string ToString()
        {
            return $"Ma SP: {Id,-10} | Ten SP: {TenSP,-20} | Gia: {Price,-10:N0} | SL: {Quantity}";
        }
    }

    // ==========================================
    // 3. GENERIC REPOSITORY
    // ==========================================

    public class Repository<T> where T : IEntity
    {
        private List<T> danhSachDuLieu = new List<T>(); // bien: danhSachDuLieu

        // Ten ham: Them
        public void Them(T doiTuong)
        {
            if (danhSachDuLieu.Any(i => i.Id.Equals(doiTuong.Id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateProductException($"Loi: Da ton tai san pham co ma '{doiTuong.Id}' trong he thong.");
            }
            danhSachDuLieu.Add(doiTuong);
        }

        // Ten ham: Xoa
        public void Xoa(string ma)
        {
            var doiTuong = TimTheoMa(ma);
            if (doiTuong == null)
            {
                throw new ProductNotFoundException($"Loi: Khong tim thay san pham co ma '{ma}' de xoa.");
            }
            danhSachDuLieu.Remove(doiTuong);
        }

        // Ten ham: TimTheoMa
        public T TimTheoMa(string ma)
        {
            return danhSachDuLieu.FirstOrDefault(i => i.Id.Equals(ma, StringComparison.OrdinalIgnoreCase));
        }

        // Ten ham: LocDuLieu
        public IEnumerable<T> LocDuLieu(Func<T, bool> dieuKienLọc)
        {
            return danhSachDuLieu.Where(dieuKienLọc);
        }

        // Ten ham: LayTatCa
        public IEnumerable<T> LayTatCa()
        {
            return danhSachDuLieu;
        }
    }

    // ==========================================
    // 4. SERVICE (XU LY NGHIEP VU & SU KIEN)
    // ==========================================

    public class ProductService
    {
        private Repository<Product> khoDuLieu = new Repository<Product>(); // bien: khoDuLieu

        // Ten event: KhiSanPhamThayDoi
        public event Action<string> KhiSanPhamThayDoi;

        // Ten ham: ThemSanPham
        public void ThemSanPham(Product sanPhamMoi)
        {
            if (string.IsNullOrWhiteSpace(sanPhamMoi.Id))
                throw new ArgumentException("Ma san pham khong duoc rong.");

            khoDuLieu.Them(sanPhamMoi);
            KhiSanPhamThayDoi?.Invoke($"Da THEM thanh cong san pham: {sanPhamMoi.TenSP} (Ma: {sanPhamMoi.Id})");
        }

        // Ten ham: XoaSanPham
        public void XoaSanPham(string maSanPham)
        {
            khoDuLieu.Xoa(maSanPham);
            KhiSanPhamThayDoi?.Invoke($"Da XOA thanh cong san pham co ma: {maSanPham}");
        }

        // Ten ham: LayDanhSachSanPham
        public IEnumerable<Product> LayDanhSachSanPham() => khoDuLieu.LayTatCa();

        // Ten ham: TimSanPhamTheoMa
        public Product TimSanPhamTheoMa(string ma) => khoDuLieu.TimTheoMa(ma);

        // Ten ham: TimSanPhamTheoTen
        public IEnumerable<Product> TimSanPhamTheoTen(string tuKhoa)
        {
            return khoDuLieu.LocDuLieu(sp => sp.TenSP.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        // Ten ham: LocTheoKhoangGia
        public IEnumerable<Product> LocTheoKhoangGia(decimal giaNhoNhat, decimal giaLonNhat)
        {
            return khoDuLieu.LocDuLieu(sp => sp.Price >= giaNhoNhat && sp.Price <= giaLonNhat);
        }

        // Ten ham: TinhTongGiaTriKho
        public decimal TinhTongGiaTriKho()
        {
            return khoDuLieu.LayTatCa().Sum(sp => sp.Price * sp.Quantity);
        }
    }

    // ==========================================
    // 5. PROGRAM (UI & DIEU KHIEN)
    // ==========================================

    class Program
    {
        static void Main(string[] args)
        {
            ProductService dichVuSanPham = new ProductService(); // bien: dichVuSanPham

            dichVuSanPham.KhiSanPhamThayDoi += thongBao =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[THONG BAO TU HE THONG]: {thongBao}");
                Console.ResetColor();
            };

            while (true)
            {
                Console.WriteLine("\n===== PRODUCT MANAGER =====");
                Console.WriteLine("1. Them san pham");
                Console.WriteLine("2. Xuat danh sach");
                Console.WriteLine("3. Tim theo ma");
                Console.WriteLine("4. Tim theo ten");
                Console.WriteLine("5. Loc theo khoang gia");
                Console.WriteLine("6. Xoa san pham");
                Console.WriteLine("7. Tinh tong gia tri kho");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon: ");

                string luaChon = Console.ReadLine(); // bien: luaChon

                try
                {
                    switch (luaChon)
                    {
                        case "1":
                            Console.Write("Nhap ma SP: ");
                            string maSP = Console.ReadLine(); // bien: maSP
                            Console.Write("Nhap ten SP: ");
                            string tenSP = Console.ReadLine(); // bien: tenSP
                            Console.Write("Nhap don gia: ");
                            decimal giaSP = decimal.Parse(Console.ReadLine()); // bien: giaSP
                            Console.Write("Nhap so luong: ");
                            int soLuongSP = int.Parse(Console.ReadLine()); // bien: soLuongSP

                            Product sanPhamMoi = new Product { Id = maSP, TenSP = tenSP, Price = giaSP, Quantity = soLuongSP }; // bien: sanPhamMoi
                            dichVuSanPham.ThemSanPham(sanPhamMoi);
                            break;

                        case "2":
                            var danhSachHienTai = dichVuSanPham.LayDanhSachSanPham().ToList(); // bien: danhSachHienTai
                            if (danhSachHienTai.Count == 0)
                            {
                                Console.WriteLine("Danh sach san pham dang rong.");
                            }
                            else
                            {
                                Console.WriteLine("\n--- DANH SACH SAN PHAM ---");
                                foreach (var sp in danhSachHienTai) Console.WriteLine(sp);
                            }
                            break;

                        case "3":
                            Console.Write("Nhap ma SP can tim: ");
                            string maCanTim = Console.ReadLine(); // bien: maCanTim
                            var spTimThay = dichVuSanPham.TimSanPhamTheoMa(maCanTim); // bien: spTimThay
                            if (spTimThay != null)
                                Console.WriteLine(spTimThay);
                            else
                                Console.WriteLine("Khong tim thay san pham.");
                            break;

                        case "4":
                            Console.Write("Nhap tu khoa ten SP: ");
                            string tuKhoa = Console.ReadLine(); // bien: tuKhoa
                            var dsTimTheoTen = dichVuSanPham.TimSanPhamTheoTen(tuKhoa).ToList(); // bien: dsTimTheoTen
                            if (dsTimTheoTen.Count > 0)
                                dsTimTheoTen.ForEach(sp => Console.WriteLine(sp));
                            else
                                Console.WriteLine("Khong tim thay san pham nao khop voi tu khoa.");
                            break;

                        case "5":
                            Console.Write("Nhap gia toi thieu: ");
                            decimal giaNhoNhat = decimal.Parse(Console.ReadLine()); // bien: giaNhoNhat
                            Console.Write("Nhap gia toi da: ");
                            decimal giaLonNhat = decimal.Parse(Console.ReadLine()); // bien: giaLonNhat

                            var dsLocTheoGia = dichVuSanPham.LocTheoKhoangGia(giaNhoNhat, giaLonNhat).ToList(); // bien: dsLocTheoGia
                            if (dsLocTheoGia.Count > 0)
                                dsLocTheoGia.ForEach(sp => Console.WriteLine(sp));
                            else
                                Console.WriteLine("Khong co san pham nao trong khoang gia nay.");
                            break;

                        case "6":
                            Console.Write("Nhap ma SP can xoa: ");
                            string maCanXoa = Console.ReadLine(); // bien: maCanXoa
                            dichVuSanPham.XoaSanPham(maCanXoa);
                            break;

                        case "7":
                            Console.WriteLine($"\nTong gia tri kho: {dichVuSanPham.TinhTongGiaTriKho():N0} VND");
                            break;

                        case "0":
                            Console.WriteLine("Dang thoat chuong trinh...");
                            return;

                        default:
                            Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                            break;
                    }
                }
                catch (DuplicateProductException ngoaiLe)
                {
                    HienThiLoi(ngoaiLe.Message);
                }
                catch (ProductNotFoundException ngoaiLe)
                {
                    HienThiLoi(ngoaiLe.Message);
                }
                catch (FormatException)
                {
                    HienThiLoi("Loi: Du lieu nhap vao phai la so hop le.");
                }
                catch (Exception ngoaiLe)
                {
                    HienThiLoi($"Loi he thong: {ngoaiLe.Message}");
                }
            }
        }

        // Ten ham: HienThiLoi
        static void HienThiLoi(string thongBaoLoi)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(thongBaoLoi);
            Console.ResetColor();
        }
    }
}
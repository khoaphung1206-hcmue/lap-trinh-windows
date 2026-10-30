using System.Globalization;
using StudentReportDemo.Models;
using StudentReportDemo.Services;

namespace StudentReportDemo.Forms
{
    public partial class FrmBaoCaoSinhVien : Form
    {
        private const string TatCaLop = "";            // MaLop dac biet = khong loc theo lop
        private const string KetQuaTatCa = "Tất cả";
        private const string KetQuaDat = "Đạt";
        private const string KetQuaKhongDat = "Không đạt";

        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        private string tieuDeBaoCao = "Báo cáo danh sách sinh viên";
        private string tomTatBaoCao = "";

        // Ten cot -> tieu de tieng Viet va dinh dang hien thi
        private static readonly Dictionary<string, string> TieuDeCot = new()
        {
            ["MaSV"] = "Mã SV",
            ["HoTen"] = "Họ tên",
            ["MaLop"] = "Mã lớp",
            ["TenLop"] = "Lớp",
            ["Diem"] = "Điểm",
            ["KetQua"] = "Kết quả",
            ["SoLuong"] = "Số lượng",
            ["SoDat"] = "Số đạt",
            ["SoKhongDat"] = "Số không đạt",
            ["DiemTrungBinh"] = "Điểm TB"
        };

        private static readonly Dictionary<string, string> DinhDangCot = new()
        {
            ["Diem"] = "0.0",
            ["DiemTrungBinh"] = "0.00"
        };

        public FrmBaoCaoSinhVien()
        {
            InitializeComponent();
        }

        // ===================== LOAD FORM =====================

        private void FrmBaoCaoSinhVien_Load(object? sender, EventArgs e)
        {
            TaoDuLieuMau();
            LoadComboBox();
            XemBaoCao(baoKhiRong: false);
        }

        private void TaoDuLieuMau()
        {
            danhSachSinhVien = new List<SinhVien>
            {
                new SinhVien { MaSV = "SV001", HoTen = "Nguyễn Văn An",     MaLop = "CTK47A", TenLop = "CNTT K47A", Diem = 8.2 },
                new SinhVien { MaSV = "SV002", HoTen = "Trần Thị Bình",     MaLop = "CTK47A", TenLop = "CNTT K47A", Diem = 6.5 },
                new SinhVien { MaSV = "SV003", HoTen = "Lê Minh Cường",     MaLop = "CTK47A", TenLop = "CNTT K47A", Diem = 4.8 },
                new SinhVien { MaSV = "SV004", HoTen = "Phạm Thanh Dung",   MaLop = "CTK47A", TenLop = "CNTT K47A", Diem = 9.1 },
                new SinhVien { MaSV = "SV005", HoTen = "Hoàng Quốc Huy",    MaLop = "CTK47A", TenLop = "CNTT K47A", Diem = 5.0 },

                new SinhVien { MaSV = "SV006", HoTen = "Võ Gia Khang",      MaLop = "CTK47B", TenLop = "CNTT K47B", Diem = 3.9 },
                new SinhVien { MaSV = "SV007", HoTen = "Đặng Minh Khôi",    MaLop = "CTK47B", TenLop = "CNTT K47B", Diem = 5.5 },
                new SinhVien { MaSV = "SV008", HoTen = "Bùi Thảo Linh",     MaLop = "CTK47B", TenLop = "CNTT K47B", Diem = 8.0 },
                new SinhVien { MaSV = "SV009", HoTen = "Lý Thu Quỳnh",      MaLop = "CTK47B", TenLop = "CNTT K47B", Diem = 7.4 },
                new SinhVien { MaSV = "SV010", HoTen = "Phan Gia Phúc",     MaLop = "CTK47B", TenLop = "CNTT K47B", Diem = 4.5 },

                new SinhVien { MaSV = "SV011", HoTen = "Ngô Tuấn Minh",     MaLop = "CTK48A", TenLop = "CNTT K48A", Diem = 6.0 },
                new SinhVien { MaSV = "SV012", HoTen = "Đỗ Bảo Ngọc",       MaLop = "CTK48A", TenLop = "CNTT K48A", Diem = 9.3 },
                new SinhVien { MaSV = "SV013", HoTen = "Huỳnh Gia Phúc",    MaLop = "CTK48A", TenLop = "CNTT K48A", Diem = 4.2 },
                new SinhVien { MaSV = "SV014", HoTen = "Mai Anh Thư",       MaLop = "CTK48A", TenLop = "CNTT K48A", Diem = 7.8 },
                new SinhVien { MaSV = "SV015", HoTen = "Vũ Đức Trung",      MaLop = "CTK48A", TenLop = "CNTT K48A", Diem = 2.7 }
            };
        }

        private void LoadComboBox()
        {
            // LINQ: lay cac lop khac nhau, sap xep theo ma lop, them muc "Tat ca cac lop" len dau
            var dsLop = danhSachSinhVien
                .Select(sv => new { sv.MaLop, sv.TenLop })
                .Distinct()
                .OrderBy(lop => lop.MaLop)
                .Prepend(new { MaLop = TatCaLop, TenLop = "Tất cả các lớp" })
                .ToList();

            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = dsLop;

            cboKetQua.Items.Clear();
            cboKetQua.Items.AddRange(new object[] { KetQuaTatCa, KetQuaDat, KetQuaKhongDat });
            cboKetQua.SelectedIndex = 0;
        }

        // ===================== XEM BAO CAO =====================

        private void btnXemBaoCao_Click(object? sender, EventArgs e)
        {
            XemBaoCao(baoKhiRong: true);
        }

        private void XemBaoCao(bool baoKhiRong)
        {
            string maLop = Convert.ToString(cboLop.SelectedValue) ?? TatCaLop;
            string ketQua = cboKetQua.SelectedItem?.ToString() ?? KetQuaTatCa;
            string tuKhoa = txtTuKhoa.Text.Trim();

            var baoCao = LocDuLieu(maLop, ketQua, tuKhoa)
                .OrderByDescending(sv => sv.Diem)
                .ThenBy(sv => sv.HoTen)
                .Select(sv => new { sv.MaSV, sv.HoTen, sv.TenLop, sv.Diem, sv.KetQua })
                .ToList();

            HienThiLenLuoi(baoCao);
            tieuDeBaoCao = $"Báo cáo sinh viên - {cboLop.Text} - Kết quả: {ketQua}" +
                           (tuKhoa.Length > 0 ? $" - Từ khóa: \"{tuKhoa}\"" : "");
            CapNhatThongKe(baoCao.Count);

            if (baoCao.Count == 0 && baoKhiRong)
            {
                MessageBox.Show("Không có sinh viên nào phù hợp với điều kiện đã chọn.",
                    "Không có dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>Loc theo lop, ket qua hoc tap va tu khoa (khong phan biet hoa thuong, dau).</summary>
        private IEnumerable<SinhVien> LocDuLieu(string maLop, string ketQua, string tuKhoa)
        {
            IEnumerable<SinhVien> query = danhSachSinhVien;

            if (maLop != TatCaLop)
                query = query.Where(sv => sv.MaLop == maLop);

            if (ketQua == KetQuaDat)
                query = query.Where(sv => sv.Diem >= 5);
            else if (ketQua == KetQuaKhongDat)
                query = query.Where(sv => sv.Diem < 5);

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                string key = TextHelper.BoDau(tuKhoa);
                query = query.Where(sv =>
                    TextHelper.BoDau(sv.MaSV).Contains(key) ||
                    TextHelper.BoDau(sv.HoTen).Contains(key));
            }

            return query;
        }

        // ===================== THONG KE =====================

        /// <summary>Cap nhat thanh trang thai: tong so, so dat, so khong dat, diem trung binh.</summary>
        private void CapNhatThongKe(int soDongHienThi)
        {
            string maLop = Convert.ToString(cboLop.SelectedValue) ?? TatCaLop;
            List<SinhVien> phamVi = danhSachSinhVien
                .Where(sv => maLop == TatCaLop || sv.MaLop == maLop)
                .ToList();

            int tongSo = phamVi.Count;
            int soDat = phamVi.Count(sv => sv.Diem >= 5);
            int soKhongDat = phamVi.Count(sv => sv.Diem < 5);
            double diemTB = tongSo > 0 ? phamVi.Average(sv => sv.Diem) : 0;

            tomTatBaoCao = $"Hiển thị: {soDongHienThi} | Tổng ({cboLop.Text}): {tongSo} | " +
                           $"Đạt: {soDat} | Không đạt: {soKhongDat} | ĐTB: {diemTB.ToString("0.00", CultureInfo.CurrentCulture)}";
            lblThongKe.Text = tomTatBaoCao;
        }

        private void btnThongKe_Click(object? sender, EventArgs e)
        {
            var thongKe = danhSachSinhVien
                .GroupBy(sv => new { sv.MaLop, sv.TenLop })
                .Select(g => new
                {
                    g.Key.MaLop,
                    g.Key.TenLop,
                    SoLuong = g.Count(),
                    SoDat = g.Count(sv => sv.Diem >= 5),
                    SoKhongDat = g.Count(sv => sv.Diem < 5),
                    DiemTrungBinh = Math.Round(g.Average(sv => sv.Diem), 2)
                })
                .OrderBy(x => x.MaLop)
                .ToList();

            HienThiLenLuoi(thongKe);

            int tongSV = thongKe.Sum(x => x.SoLuong);
            double dtbChung = danhSachSinhVien.Average(sv => sv.Diem);
            tieuDeBaoCao = "Thống kê sinh viên theo lớp";
            tomTatBaoCao = $"Thống kê theo lớp: {thongKe.Count} lớp | {tongSV} sinh viên | " +
                           $"ĐTB chung: {dtbChung.ToString("0.00", CultureInfo.CurrentCulture)}";
            lblThongKe.Text = tomTatBaoCao;
        }

        // ===================== HIEN THI LUOI =====================

        private void HienThiLenLuoi<T>(List<T> duLieu)
        {
            dgvBaoCao.DataSource = null;
            dgvBaoCao.DataSource = duLieu;

            foreach (DataGridViewColumn cot in dgvBaoCao.Columns)
            {
                if (TieuDeCot.TryGetValue(cot.Name, out string? tieuDe))
                    cot.HeaderText = tieuDe;
                if (DinhDangCot.TryGetValue(cot.Name, out string? dinhDang))
                {
                    cot.DefaultCellStyle.Format = dinhDang;
                    cot.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        // Tô đỏ chữ "Không đạt" cho dễ nhìn
        private void dgvBaoCao_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvBaoCao.Columns[e.ColumnIndex].Name != "KetQua") return;

            e.CellStyle!.ForeColor = Convert.ToString(e.Value) == KetQuaKhongDat ? Color.Red : Color.DarkGreen;
        }

        // ===================== XUAT FILE =====================

        private void btnXuatFile_Click(object? sender, EventArgs e)
        {
            XuatFile();
        }

        private void XuatFile()
        {
            if (dgvBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất. Hãy bấm \"Xem báo cáo\" với điều kiện có kết quả.",
                    "Không có dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog hopThoaiLuu = new SaveFileDialog
            {
                Title = "Xuất báo cáo",
                Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = dgvBaoCao.Columns.Contains("SoLuong") ? "ThongKeTheoLop" : "BaoCaoSinhVien"
            };
            if (hopThoaiLuu.ShowDialog() != DialogResult.OK) return;

            try
            {
                LayDuLieuDangHienThi(out List<string> tieuDeCot, out List<string[]> cacDong);
                BaoCaoExporter.Xuat(hopThoaiLuu.FileName, tieuDeBaoCao, tomTatBaoCao, tieuDeCot, cacDong);

                lblThongKe.Text = $"Đã xuất {cacDong.Count} dòng ra: {hopThoaiLuu.FileName}";
                MessageBox.Show($"Xuất file thành công ({cacDong.Count} dòng).\n{hopThoaiLuu.FileName}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Không có quyền ghi vào vị trí đã chọn. Hãy chọn thư mục khác.",
                    "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Không ghi được file (có thể file đang được mở bằng Excel).\n\n" + ex.Message,
                    "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message,
                    "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Doc dung du lieu dang hien thi tren luoi (da loc/sap xep), dinh dang so khong phu thuoc vung.</summary>
        private void LayDuLieuDangHienThi(out List<string> tieuDeCot, out List<string[]> cacDong)
        {
            List<DataGridViewColumn> cotHienThi = dgvBaoCao.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            tieuDeCot = cotHienThi.Select(c => c.HeaderText).ToList();
            cacDong = dgvBaoCao.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Select(r => cotHienThi
                    .Select(c => DinhDangGiaTri(r.Cells[c.Index].Value, c.DefaultCellStyle.Format))
                    .ToArray())
                .ToList();
        }

        private static string DinhDangGiaTri(object? value, string format)
        {
            return value switch
            {
                null => "",
                IFormattable f => f.ToString(string.IsNullOrEmpty(format) ? null : format, CultureInfo.InvariantCulture),
                _ => value.ToString() ?? ""
            };
        }
    }
}

using System.Text;

namespace StudentReportDemo.Services
{
    /// <summary>
    /// Tao noi dung va ghi file bao cao (CSV hoac TXT). Khong phu thuoc giao dien.
    /// </summary>
    public static class BaoCaoExporter
    {
        public static void Xuat(string duongDan, string tieuDe, string tomTat,
                                IReadOnlyList<string> tieuDeCot, IReadOnlyList<string[]> cacDong)
        {
            bool laTxt = string.Equals(Path.GetExtension(duongDan), ".txt", StringComparison.OrdinalIgnoreCase);
            string noiDung = laTxt
                ? TaoNoiDungTxt(tieuDe, tomTat, tieuDeCot, cacDong)
                : TaoNoiDungCsv(tieuDeCot, cacDong);

            // UTF-8 co BOM de Excel/Notepad doc dung tieng Viet
            File.WriteAllText(duongDan, noiDung, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
        }

        public static string TaoNoiDungCsv(IReadOnlyList<string> tieuDeCot, IReadOnlyList<string[]> cacDong)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(",", tieuDeCot.Select(EscapeCsv)));
            foreach (string[] dong in cacDong)
                sb.AppendLine(string.Join(",", dong.Select(EscapeCsv)));
            return sb.ToString();
        }

        public static string TaoNoiDungTxt(string tieuDe, string tomTat,
                                           IReadOnlyList<string> tieuDeCot, IReadOnlyList<string[]> cacDong)
        {
            int soCot = tieuDeCot.Count;
            int[] doRong = new int[soCot];
            for (int i = 0; i < soCot; i++)
            {
                doRong[i] = Math.Max(tieuDeCot[i].Length,
                                     cacDong.Select(d => d[i].Length).DefaultIfEmpty(0).Max());
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tieuDe.ToUpperInvariant());
            sb.AppendLine($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine(new string('=', doRong.Sum() + (soCot - 1) * 3));
            sb.AppendLine(DinhDangDong(tieuDeCot, doRong));
            sb.AppendLine(new string('-', doRong.Sum() + (soCot - 1) * 3));
            foreach (string[] dong in cacDong)
                sb.AppendLine(DinhDangDong(dong, doRong));
            sb.AppendLine(new string('=', doRong.Sum() + (soCot - 1) * 3));
            if (!string.IsNullOrWhiteSpace(tomTat))
                sb.AppendLine(tomTat);
            return sb.ToString();
        }

        private static string DinhDangDong(IReadOnlyList<string> cells, int[] doRong)
        {
            return string.Join(" | ", cells.Select((c, i) => c.PadRight(doRong[i])));
        }

        // Bao ve dau phay, dau nhay kep va xuong dong trong noi dung CSV
        private static string EscapeCsv(string value)
        {
            if (value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}

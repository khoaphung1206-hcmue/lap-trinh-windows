using System.Globalization;
using System.Text;

namespace StudentReportDemo.Services
{
    public static class TextHelper
    {
        /// <summary>
        /// Bo dau tieng Viet va doi thanh chu thuong: "Nguyễn Đăng" -> "nguyen dang".
        /// Dung de tim kiem khong phan biet hoa thuong va dau.
        /// </summary>
        public static string BoDau(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder(normalized.Length);
            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString()
                     .Normalize(NormalizationForm.FormC)
                     .Replace('đ', 'd')
                     .Replace('Đ', 'D')
                     .ToLowerInvariant();
        }
    }
}

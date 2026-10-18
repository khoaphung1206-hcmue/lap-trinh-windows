namespace StudentCardPrinter
{
    /// <summary>
    /// Dữ liệu thông tin sinh viên in trên thẻ.
    /// Dùng chung cho vẽ preview trên Panel, in ấn và lưu PNG.
    /// </summary>
    public class StudentCardInfo
    {
        public string StudentId { get; set; } = "";
        public string FullName { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Course { get; set; } = "";
        public DateTime BirthDate { get; set; } = DateTime.Today;
        public string Gender { get; set; } = "";
        public Image Photo { get; set; }
    }
}

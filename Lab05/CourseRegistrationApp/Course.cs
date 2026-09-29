namespace CourseRegistrationApp
{
    /// <summary>
    /// Thông tin một khóa học: tên và học phí mỗi tháng.
    /// </summary>
    public class Course
    {
        public string Name { get; }
        public decimal PricePerMonth { get; }

        public Course(string name, decimal pricePerMonth)
        {
            Name = name;
            PricePerMonth = pricePerMonth;
        }

        // ComboBox hiển thị tên khóa học thông qua ToString()
        public override string ToString()
        {
            return Name;
        }
    }
}

namespace ShopManagerMini.Models
{
    /// <summary>
    /// Khách hàng của cửa hàng.
    /// </summary>
    public class Customer
    {
        public string MaKH { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string LoaiKH { get; set; } = "";

        public Customer()
        {
        }

        public Customer(string maKH, string hoTen, string soDienThoai, string loaiKH)
        {
            MaKH = maKH;
            HoTen = hoTen;
            SoDienThoai = soDienThoai;
            LoaiKH = loaiKH;
        }
    }
}

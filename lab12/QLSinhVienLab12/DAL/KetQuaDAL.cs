using System.Data;
using Microsoft.Data.SqlClient;

namespace QLSinhVienLab12.DAL
{
    /// <summary>
    /// Tang truy xuat du lieu: chi goi stored procedure bang tham so, khong noi chuoi SQL.
    /// </summary>
    public class KetQuaDAL
    {
        public DataTable GetLop()
        {
            return ExecuteTable("sp_Lop_GetAll");
        }

        public DataTable GetMonHoc()
        {
            return ExecuteTable("sp_MonHoc_GetAll");
        }

        public DataTable GetKetQuaByLopMon(string maLop, string maMon)
        {
            return ExecuteTable("sp_KetQua_GetByLopMon",
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = maLop },
                new SqlParameter("@MaMon", SqlDbType.NVarChar, 20) { Value = maMon });
        }

        /// <summary>
        /// Luu nhieu dong trong MOT transaction. Co loi o bat ky dong nao -> Rollback toan bo.
        /// </summary>
        /// <returns>So dong da luu.</returns>
        public int SaveAll(string maMon, DataTable table)
        {
            using SqlConnection conn = new SqlConnection(Database.ConnectionString);
            conn.Open();
            using SqlTransaction trans = conn.BeginTransaction();

            try
            {
                using SqlCommand cmd = new SqlCommand("sp_KetQua_Save", conn, trans)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar, 20);
                cmd.Parameters.Add("@MaMon", SqlDbType.NVarChar, 20).Value = maMon;
                cmd.Parameters.Add("@Diem", SqlDbType.Float);
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 255);

                int count = 0;
                foreach (DataRow row in table.Rows)
                {
                    string maSV = Convert.ToString(row["MaSV"]) ?? "";

                    // Lop bao ve thu 2 (Form da validate truoc): sai la nem loi -> Rollback
                    if (row.IsNull("Diem"))
                        throw new InvalidDataException($"Điểm của sinh viên {maSV} đang để trống.");
                    double diem = Convert.ToDouble(row["Diem"]);
                    if (double.IsNaN(diem) || diem < 0 || diem > 10)
                        throw new InvalidDataException($"Điểm của sinh viên {maSV} phải nằm trong khoảng 0 đến 10.");

                    cmd.Parameters["@MaSV"].Value = maSV;
                    cmd.Parameters["@Diem"].Value = diem;
                    cmd.Parameters["@GhiChu"].Value = (Convert.ToString(row["GhiChu"]) ?? "").Trim();
                    cmd.ExecuteNonQuery();
                    count++;
                }

                trans.Commit();   // chi commit 1 lan, SAU KHI moi dong da thanh cong
                return count;
            }
            catch
            {
                try { trans.Rollback(); } catch { /* ket noi da dong, khong lam mat loi goc */ }
                throw;
            }
        }

        private static DataTable ExecuteTable(string procName, params SqlParameter[] parameters)
        {
            using SqlConnection conn = new SqlConnection(Database.ConnectionString);
            using SqlCommand cmd = new SqlCommand(procName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddRange(parameters);

            using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}

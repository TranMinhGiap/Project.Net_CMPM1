using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbCustomer
    {
        private Connect connect = new Connect();
        #region Lấy dữ liệu khách hàng
        public DataSet GetCustomer()
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Customers";
            DataSet ds = new DataSet();
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu khách hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Chèn dữ liệu khách hàng vào database
        public void InsertCustomer(string id, string name, string address, string phone, string email)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Customers VALUES (@id, @name, @address, @phone, @email)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu khách hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Cập nhật dữ liệu khách hàng
        public void UpdateCustomer(string id, string name, string address, string phone, string email)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Customers SET CustomerName = @name, CustomerAddress = @address, CustomerPhone = @phone, CustomerEmail = @email WHERE CustomerID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật khách hàng:" + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Xóa khách hàng
        public void DeleteCustomer(string id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "DELETE FROM Customers WHERE CustomerID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa khách hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Tìm kiếm khách hàng theo mã
        public DataSet FindCustomerID(string id)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Customers WHERE CustomerID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm khách hàng theo mã: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Tìm kiếm nhà cung cấp theo tên
        public DataSet FindCustomerName(string name)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Customers WHERE CustomerName LIKE @name";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm khách hàng theo tên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Lấy số lượng khách hàng
        public int GetCustomerCount()
        {
            int count = 0;
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT COUNT(*) FROM Customers";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy số lượng khách hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return count;
        }

        #endregion
    }
}

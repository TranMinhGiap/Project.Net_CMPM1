using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang.Controller
{
    internal class dbSupplier
    {
        private Connect connect = new Connect();
        #region Lấy dữ liệu nhà cung cấp
        public DataSet GetSupplier()
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Suppliers";
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
                Console.WriteLine("Lỗi khi lấy dữ liệu nhà cung cấp: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Chèn dữ liệu nhà cung cấp vào database
        public void InsertSupplier(string id, string name, string address, string phone, string email)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Suppliers VALUES (@id, @name, @address, @phone, @email)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue ("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu nhà cung cấp: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Cập nhật dữ liệu nhà cung cấp
        public void UpdateSupplier(string id, string name, string address,string phone, string email)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Suppliers SET SupplierName = @name, SupplierAddress = @address, SupplierPhone = @phone, SupplierEmail = @email WHERE SupplierID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand (sql, dbConnect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            } catch(Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật nhà cung cấp" + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Xóa nhà cung cấp
        public void DeleteSupplier(string id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            //string sql = "DELETE FROM Suppliers WHERE SupplierID = @id";
            SqlTransaction transaction = null;
            try
            {
                dbConnect.Open();
                // Bắt đầu giao dịch
                transaction = dbConnect.BeginTransaction();

                // Cập nhật bảng Products để gán CategoryID thành NULL
                string updateProduct = "UPDATE Products SET SupplierID = NULL WHERE SupplierID = @id";
                SqlCommand updateCmd = new SqlCommand(updateProduct, dbConnect, transaction);
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.ExecuteNonQuery();

                // Thực hiện xóa nhà cung cấp
                string deleteSql = "DELETE FROM Suppliers WHERE SupplierID = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, dbConnect, transaction);
                deleteCmd.Parameters.AddWithValue("@id", id);
                deleteCmd.ExecuteNonQuery();

                // Commit giao dịch nếu không có lỗi
                transaction.Commit();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa nhà cung cấp!" + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Tìm kiếm nhà cung cấp theo mã
        public DataSet FindSupplierID(string id)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Suppliers WHERE SupplierID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm nhà cung cấp theo mã: " +  ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Tìm kiếm nhà cung cấp theo tên
        public DataSet FindSupplierName(string name)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Suppliers WHERE SupplierName LIKE @name";
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
                Console.WriteLine("Lỗi khi tìm nhà cung cấp theo tên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
    }
}

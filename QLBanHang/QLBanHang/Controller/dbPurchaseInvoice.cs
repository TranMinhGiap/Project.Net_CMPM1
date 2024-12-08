using QLBanHang.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbPurchaseInvoice
    {
        private Connect connect = new Connect();
        #region chèn dữ liệu hóa đơn 
        public void InsertPurchaseInvoice(string id, int employeeid, DateTime date, decimal price)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO PurchaseInvoices VALUES (@id, @employeeid, @date, @price)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@employeeid", employeeid);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu hóa đơn nhập: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region lấy dữ liệu hóa đơn
        public DataSet GetPurchaseInvoice()
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = "SELECT PurchaseInvoiceID, EmployeeName, InvoiceDate, TotalAmount FROM PurchaseInvoices, Employees WHERE PurchaseInvoices.EmployeeID = Employees.EmployeeID";
            try
            {
                dbconnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbconnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { dbconnect.Close(); }
            return ds;
        }
        #endregion
        #region Xóa hóa đơn
        public void DeletePurrchaseInvoice(string id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sqlPurchase = "DELETE FROM PurchaseInvoices WHERE PurchaseInvoiceID = @id";
            string sqlInvoiceDetail = "DELETE FROM InvoiceDetails WHERE PurchaseInvoiceID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmdInvoiceDetail = new SqlCommand(sqlInvoiceDetail, dbConnect);
                cmdInvoiceDetail.Parameters.AddWithValue("@id", id);
                cmdInvoiceDetail.ExecuteNonQuery();

                SqlCommand cmdPurchase = new SqlCommand(sqlPurchase, dbConnect);
                cmdPurchase.Parameters.AddWithValue("@id", id);
                cmdPurchase.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write("Lỗi xóa hóa đơn nhập: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region lấy dữ liệu hóa đơn theo mã
        public DataSet GetPurchaseInvoiceID(string id)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = "SELECT PurchaseInvoiceID, EmployeeName, InvoiceDate, TotalAmount FROM PurchaseInvoices, Employees WHERE PurchaseInvoices.EmployeeID = Employees.EmployeeID AND PurchaseInvoiceID = @id";
            try
            {
                dbconnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbconnect);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex) { Console.WriteLine("Lỗi lấy dữ liệu theo mã hóa đơn: " + ex.Message); }
            finally { dbconnect.Close(); }
            return ds;
        }
        #endregion
        #region lấy dữ liệu hóa đơn theo ngày
        public DataSet GetPurchaseInvoiceDate(string datePart)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = @"SELECT PurchaseInvoiceID, EmployeeName, InvoiceDate, TotalAmount 
                           FROM PurchaseInvoices,Employees 
                           WHERE PurchaseInvoices.EmployeeID = Employees.EmployeeID AND CONVERT(VARCHAR(10), InvoiceDate, 103) LIKE @date ";
            try
            {
                dbconnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbconnect);
                cmd.Parameters.AddWithValue("@date", "%" + datePart + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu theo ngày: " + ex.Message);
            }
            finally
            {
                dbconnect.Close();
            }
            return ds;
        }

        #endregion
    }
}

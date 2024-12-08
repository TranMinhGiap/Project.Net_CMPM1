using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class dbSell
    {
        private Connect connect = new Connect();
        #region lấy dữ liệu hóa đơn bán
        public DataSet GetSell()
        {
            DataSet dataSet = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT SellID, EmployeeName, CustomerName, SellPrice, DateOut FROM Sells, Employees, Customers WHERE Sells.EmployeeID = Employees.EmployeeID AND Sells.CustomerID = Customers.CustomerID";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu hóa đơn: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return dataSet;
        }
        #endregion
        #region Chèn dữ liệu hóa đơn bán
        public void InsertSell(string id, int employeeid, string customerid, decimal sellprice, DateTime date)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Sells VALUES (@id, @employeeid, @customerid, @sellprice, @date)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@employeeid", employeeid);
                cmd.Parameters.AddWithValue("customerid", customerid);
                cmd.Parameters.AddWithValue("@sellprice", sellprice);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu hóa đơn bán hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region lấy dữ liệu hóa đơn bán theo mã
        public DataSet GetSellID(string id)
        {
            DataSet dataSet = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT SellID, EmployeeName, CustomerName, SellPrice, DateOut FROM Sells, Employees, Customers WHERE Sells.EmployeeID = Employees.EmployeeID AND Sells.CustomerID = Customers.CustomerID AND SellID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu hóa đơn: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return dataSet;
        }
        #endregion
        #region Lấy dữ liệu theo ngày bán
        public DataSet GetSellDate(string datePart)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = @"SELECT SellID, EmployeeName, CustomerName, SellPrice, DateOut 
                           FROM Sells, Employees, Customers 
                           WHERE Sells.EmployeeID = Employees.EmployeeID AND Sells.CustomerID = Customers.CustomerID AND CONVERT(VARCHAR(10), DateOut, 103) LIKE @date ";
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
        #region Xóa hóa đơn
        public void DeleteSell(string id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sqlSell = "DELETE FROM Sells WHERE SellID = @id";
            string sqlSellDetail = "DELETE FROM SellDetail WHERE SellID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmdInvoiceDetail = new SqlCommand(sqlSellDetail, dbConnect);
                cmdInvoiceDetail.Parameters.AddWithValue("@id", id);
                cmdInvoiceDetail.ExecuteNonQuery();

                SqlCommand cmdPurchase = new SqlCommand(sqlSell, dbConnect);
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
        #region lấy hóa đơn theo ngày
        public DataSet GetSellDateDay()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT DateOut, SUM(SellPrice) AS TotalPrice FROM Sells WHERE DateOut = @date GROUP BY DateOut";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@date", DateTime.Today);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu theo ngày hiện tại: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region lấy hóa đơn theo 7 ngày
        public DataSet GetSellDate7Day()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT DateOut, SUM(SellPrice) AS TotalPrice FROM Sells WHERE DateOut >= DATEADD(DAY, -7, GETDATE()) GROUP BY DateOut ORDER BY DateOut";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu trong 7 ngày qua: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region lấy hóa đơn theo tháng
        public DataSet GetSellDateMonth()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT DateOut, SUM(SellPrice) AS TotalPrice FROM Sells WHERE MONTH(DateOut) = @month AND YEAR(DateOut) = @year GROUP BY DateOut ORDER BY DateOut";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@month", DateTime.UtcNow.Month);
                cmd.Parameters.AddWithValue("@year", DateTime.UtcNow.Year);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu theo tháng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region lấy hóa đơn theo năm
        public DataSet GetSellDateYear()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT DateOut, SUM(SellPrice) AS TotalPrice FROM Sells WHERE YEAR(DateOut) = 2024 GROUP BY DateOut ORDER BY DateOut";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu theo năm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Lấy doanh thu
        public decimal GetTotalRevenue()
        {
            decimal totalRevenue = 0;
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT SUM(SellPrice) FROM Sells"; 
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                totalRevenue = (decimal)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy tổng doanh thu: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return totalRevenue;
        }

        #endregion
    }
}

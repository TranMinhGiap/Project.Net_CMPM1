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
    internal class dbSellDetail
    {
        private Connect connect = new Connect();
        #region Chèn dữ liệu chi tiết hóa đơn
        public void InsertSellDetail(string id, string productid, int quantity, decimal price)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO  SellDetail(SellID, ProductID, Quantity, TotalPrice) VALUES (@id, @productid, @quantity, @price)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@productid", productid);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu chi tiết hóa đơn bán hàng: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Lấy dữ liệu hóa đơn và chi tiết hóa đơn
        public DataSet GetSellDetail(string id)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = "Select SellDetail.ProductID, SellDetail.Quantity, Categories.CategoryName, Suppliers.SupplierName, SellDetail.TotalPrice" +
                " FROM SellDetail, Products, Categories, Suppliers" +
                " WHERE SellDetail.ProductID = Products.ProductID AND Products.SupplierID = Suppliers.SupplierID AND Products.CategoryID = Categories.CategoryID AND SellDetail.SellID = @id";


            try
            {
                dbconnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbconnect);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu chi tiết hóa đơn: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally { dbconnect.Close(); }
            return ds;
        }
        #endregion
    }
}

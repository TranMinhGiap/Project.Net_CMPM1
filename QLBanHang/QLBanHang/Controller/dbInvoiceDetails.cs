using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace QLBanHang.Controller
{
    internal class dbInvoiceDetails
    {
        private Connect connect = new Connect();
        #region Chèn dữ liệu chi tiết hóa đơn
        public void InsertInvoiceDetail(string id, string productid, int quantity, decimal unitprice)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO InvoiceDetails (PurchaseInvoiceID, ProductID, Quantity, UnitPrice) VALUES (@id, @productid, @quantity, @unitprice)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@productid", productid);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@unitprice", unitprice);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu chi tiết hóa đơn nhập: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Lấy dữ liệu hóa đơn và chi tiết hóa đơn
        public DataSet GetPurchaseInvoiceDetail(string id)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconnect = connect.GetConnect();
            string sql = "Select InvoiceDetails.ProductID, InvoiceDetails.Quantity, Categories.CategoryName, Suppliers.SupplierName, InvoiceDetails.UnitPrice" +
                " FROM InvoiceDetails, Products, Categories, Suppliers" +
                " WHERE InvoiceDetails.ProductID = Products.ProductID AND Products.SupplierID = Suppliers.SupplierID AND Products.CategoryID = Categories.CategoryID AND InvoiceDetails.PurchaseInvoiceID = @id";


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

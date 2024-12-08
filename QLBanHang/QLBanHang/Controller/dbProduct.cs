using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QLBanHang.Controller
{
    internal class dbProduct
    {
        private Connect connect = new Connect();
        #region Lấy dữ liệu sản phẩm từ database
        public DataSet GetProduct()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Products";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Chèn dữ liệu sản phẩm vào database
        public void InsertProduct(string id, string name, int quantity, string categoryid, string supplierid,decimal price, string status, string description, string image)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Products VALUES ( @id, @name, @quantity, @categoryid, @supplierid, @price, @status, @description, @image)";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand (sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue ("@categoryid", categoryid);
                cmd.Parameters.AddWithValue("@supplierid", supplierid);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Cập nhật dữ liệu sản phẩm
        public void UpdateProduct(string id, string name, int quantity, string categoryid, string supplierid,decimal price, string status, string description, string image)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Products SET ProductName = @name, ProductQuantity = @quantity, CategoryID = @categoryid, SupplierID = @supplierid, ProductPrice = @price, ProductStatus = @status, ProductDescription = @description, ProductImage = @image WHERE ProductID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@categoryid", categoryid);
                cmd.Parameters.AddWithValue("supplierid", supplierid);
                cmd.Parameters.AddWithValue("@price", price );
                cmd.Parameters.AddWithValue("@status", status );
                cmd.Parameters.AddWithValue ("@description", description);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật dữ liệu sản phẩm: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Xóa sản phẩm
        public void ProductDelete(string id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "DELETE FROM Products WHERE ProductID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Lấy sản phẩm có giá lớn hơn hoặc bằng
        public DataSet GetProductPriceMax500(decimal price)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Products WHERE ProductPrice >= @price";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand (sql, dbConnect);
                cmd.Parameters.AddWithValue("@price", price);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi hiện thị sản phẩm giá >= price: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region tìm kiếm sản phẩm theo mã
        public DataSet GetProductID(string productID)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Products WHERE ProductID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", productID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch( Exception ex)
            {
                Console.WriteLine("Lỗi hiện thị sản phẩm theo ID: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region tìm kiếm sản phẩm theo tên
        public DataSet GetProductName(string productName)
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Products WHERE ProductName LIKE @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", "%" + productName + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi hiện thị sản phẩm theo tên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region tìm kiếm sản phẩm theo trạng thái
        public DataSet GetProductStatus()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Products WHERE ProductStatus = @status";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@status", "Đang bán");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi hiện thị sản phẩm theo trạng thái: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region tìm kiếm sản phẩm theo trạng thái chưa bán
        public DataSet GetProductStatus_SELL()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Products WHERE ProductStatus = @status";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@status", "Chưa bán");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi hiện thị sản phẩm theo trạng thái: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Lấy dữ liệu sản phẩm với tên nhà cung cấp và tên danh mục
        public DataSet GetProductCategoySupplier()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT ProductID, ProductName, ProductQuantity, Categories.CategoryName, Suppliers.SupplierName , ProductImage FROM Products, Categories, Suppliers " +
                "WHERE Products.CategoryID = Categories.CategoryID AND Products.SupplierID = Suppliers.SupplierID";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu sản phẩm với tên danh mục và tên nhà cung cấp: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Update số lượng kho khi nhập
        public void UpdateProductQuantity(string id, int quantity)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Products SET ProductQuantity = @quantity WHERE ProductID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật số lượng sản phẩm:" + ex.Message);
            }
            finally { dbConnect.Close(); }
        }
        #endregion
        #region Lấy dữ liệu sản phẩm đang bán
        public DataSet GetProductCategoySupplierStatus()
        {
            DataSet ds = new DataSet();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT ProductID, ProductName, ProductQuantity, Categories.CategoryName, Suppliers.SupplierName , ProductImage, Products.ProductPrice FROM Products, Categories, Suppliers " +
                "WHERE Products.CategoryID = Categories.CategoryID AND Products.SupplierID = Suppliers.SupplierID AND ProductStatus = @status";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@status", "Đang bán");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu sản phẩm với tên danh mục và tên nhà cung cấp: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return ds;
        }
        #endregion
        #region Update số lượng kho khi bán
        public void UpdateProductQuantitySell(string id, int quantity)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Products SET ProductQuantity = @quantity WHERE ProductID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật số lượng sản phẩm:" + ex.Message);
            }
            finally { dbConnect.Close(); }
        }
        #endregion
    }
}

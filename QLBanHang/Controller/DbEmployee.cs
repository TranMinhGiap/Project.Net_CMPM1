using QLBanHang.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    internal class DbEmployee
    {
        private Connect connect = new Connect();
        #region Lấy dữ liệu của tất cả nhân viên từ database
        public List<Employee> GetEmployee()
        {
            List<Employee> Employees = new List<Employee>();
            SqlConnection dbConnect = connect.GetConnect();
            try
            {
                string sql = "SELECT * FROM EMployees";
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect); // Tạo Sql command
                SqlDataReader rd = cmd.ExecuteReader(); //Đọc dữ liệu
                while (rd.Read())
                {
                    Employee employee = new Employee();
                    employee.Employeeid = rd.GetInt32(0);
                    employee.Employeeuser = rd.GetString(1);
                    employee.Employeepass = rd.GetString(2);
                    employee.EmployeeName = rd.GetString(3);
                    employee.Gender = rd.GetString(4);
                    employee.Role = rd.GetString(5);
                    employee.EmployeePhone = rd.GetString(6);
                    employee.Birthday = rd.GetDateTime(7);
                    employee.EmployeeAddress = rd.GetString(8);
                    employee.EmployeeEmail = rd.GetString(9);
                    employee.Employeeimage = rd.GetString(10);
                    Employees.Add(employee);
                }
                return Employees;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu nhân viên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return null;
        }
        #endregion
        #region Chèn dữ liệu nhân viên vào database
        public bool InsertEmployee(string user, string pass, string name, string gender, string role, string phone, DateTime birthday, string address, string email, string image)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "INSERT INTO Employees (EmployeeUser, EmployeePass, EmployeeName, EmployeeGender, EmployeeRole, EmployeePhone, EmployeeBirthday, EmployeeAddress, EmployeeEmail, EmployeeImage) " +
             "VALUES (@user, @pass, @name, @gender, @role, @phone, @birthday, @address, @email, @image)";

            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@birthday", birthday);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chèn dữ liệu nhân viên: " + ex.Message);
                return false;
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Cập nhật dữ liệu nhân viên
        public bool UpdateEmployee(string user, string pass, string name, string gender, string role, string phone, DateTime birthday, string address, string email, string image, int id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Employees" +
             " SET EmployeeUser = @user, EmployeePass = @pass,EmployeeName = @name, EmployeeGender = @gender, EmployeeRole = @role, EmployeePhone = @phone, EmployeeBirthday = @birthday,EmployeeAddress = @address, EmployeeEmail = @email, EmployeeImage = @image"
             + " WHERE EmployeeID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@birthday", birthday);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật dữ liệu nhân viên: " + ex.Message);
                return false;
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Xóa dữ liệu nhân viên
        public bool DeleteEmployee(int id)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "DELETE Employees WHERE EmployeeID = @id";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa dữ liệu nhân viên: " + ex.Message);
                return false;
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Đổi mật khẩu nhân viên qua email
        public bool SetPassword(string email, string password)
        {
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "UPDATE Employees SET EmployeePass = @pass WHERE EmployeeEmail = @email";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi update password: " + ex.Message);
                return false;
            }
            finally
            {
                dbConnect.Close();
            }
        }
        #endregion
        #region Lấy dữ liệu nhân viên từ database
        public List<Employee> GetEmployeeRole()
        {
            List<Employee> Employees = new List<Employee>();
            SqlConnection dbConnect = connect.GetConnect();
            try
            {
                string sql = "SELECT * FROM EMployees WHERE EmployeeRole = @role";
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect); // Tạo Sql command
                cmd.Parameters.AddWithValue("@role", "Nhân viên");
                SqlDataReader rd = cmd.ExecuteReader(); //Đọc dữ liệu
                while (rd.Read())
                {
                    Employee employee = new Employee();
                    employee.Employeeid = rd.GetInt32(0);
                    employee.Employeeuser = rd.GetString(1);
                    employee.Employeepass = rd.GetString(2);
                    employee.EmployeeName = rd.GetString(3);
                    employee.Gender = rd.GetString(4);
                    employee.Role = rd.GetString(5);
                    employee.EmployeePhone = rd.GetString(6);
                    employee.Birthday = rd.GetDateTime(7);
                    employee.EmployeeAddress = rd.GetString(8);
                    employee.EmployeeEmail = rd.GetString(9);
                    employee.Employeeimage = rd.GetString(10);
                    Employees.Add(employee);
                }
                return Employees;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu nhân viên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return null;
        }
        #endregion
        #region Tìm kiếm nhân viên theo tên
        public List<Employee> FindEmployeeName(string name, string role)
        {
            List<Employee> Employees = new List<Employee>();
            SqlConnection dbConnect = connect.GetConnect();
            string sql;
            if (role.Equals("Admin"))
            {
                sql = "SELECT * FROM Employees WHERE EmployeeName LIKE @name";
            }
            else
            {
                sql = "SELECT * FROM Employees WHERE EmployeeName LIKE @name AND EmployeeRole LIKE @role";
            }
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@name", name);
                if (!role.Equals("Admin"))
                {
                    cmd.Parameters.AddWithValue("@role", "Nhân viên");
                }
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Employee employee = new Employee();
                    employee.Employeeid = rd.GetInt32(0);
                    employee.Employeeuser = rd.GetString(1);
                    employee.Employeepass = rd.GetString(2);
                    employee.EmployeeName = rd.GetString(3);
                    employee.Gender = rd.GetString(4);
                    employee.Role = rd.GetString(5);
                    employee.EmployeePhone = rd.GetString(6);
                    employee.Birthday = rd.GetDateTime(7);
                    employee.EmployeeAddress = rd.GetString(8);
                    employee.EmployeeEmail = rd.GetString(9);
                    employee.Employeeimage = rd.GetString(10);
                    Employees.Add(employee);
                }
                return Employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm nhân viên theo tên: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return null;
        }
        #endregion
        #region Tìm kiếm theo chức vụ
        public List<Employee> FindEmployeeRole(string role)
        {
            List<Employee> Employees = new List<Employee>();
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT * FROM Employees WHERE EmployeeRole  LIKE @role";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                cmd.Parameters.AddWithValue("@role", role);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Employee employee = new Employee();
                    employee.Employeeid = rd.GetInt32(0);
                    employee.Employeeuser = rd.GetString(1);
                    employee.Employeepass = rd.GetString(2);
                    employee.EmployeeName = rd.GetString(3);
                    employee.Gender = rd.GetString(4);
                    employee.Role = rd.GetString(5);
                    employee.EmployeePhone = rd.GetString(6);
                    employee.Birthday = rd.GetDateTime(7);
                    employee.EmployeeAddress = rd.GetString(8);
                    employee.EmployeeEmail = rd.GetString(9);
                    employee.Employeeimage = rd.GetString(10);
                    Employees.Add(employee);
                }
                return Employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tìm kiếm nhân viên theo chức vụ: " + ex.Message);
            }
            finally
            {
                dbConnect.Close();
            }
            return null;
        }
        #endregion
        #region Lấy số lượng nhân viên
        public int GetEmployeeCount()
        {
            int count = 0;
            SqlConnection dbConnect = connect.GetConnect();
            string sql = "SELECT COUNT(*) FROM Employees";
            try
            {
                dbConnect.Open();
                SqlCommand cmd = new SqlCommand(sql, dbConnect);
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy số lượng nhân viên: " + ex.Message);
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

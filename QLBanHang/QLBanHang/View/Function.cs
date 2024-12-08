using Guna.UI2.WinForms;
using QLBanHang.Controller;
using QLBanHang.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLBanHang.View
{
    public partial class Function : Form
    {
        private string imageEmployee = "D:\\images-NET\\user (1).png";
        private string imageProduct = "D:\\images-NET\\product.jpg";
        private Employee employee = new Employee();
        public Function(Employee employ)
        {
            InitializeComponent();
            employee = employ;
            PhanQuyen();
            btnTrangChu_Click(this, EventArgs.Empty);
            cboChart_SelectedIndexChanged(this, EventArgs.Empty);
        }

        #region Employee
        //Phân quyền
        void PhanQuyen()
        {
            if (employee.Role.Equals("Admin"))
            {
                LoadAllDataEmployee();
            }
            else if (employee.Role.Equals("Quản lý"))
            {
                LoadDataEmployeeRole();
                cboFindEmployee.Enabled = false;
            }
            else
            {
                btnQuanLyNhanVien.Visible = false;
                pnQuanLyNhanVien.Visible = false;
                pnThemNhanVien.Visible = false;
            }
            ptbImageEmployee.Image = Image.FromFile(employee.Employeeimage);
            txtName.Text = employee.EmployeeName;
            txtRole.Text = employee.Role;
        }
        // Load ALL Data Employee
        void LoadAllDataEmployee()
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = dbEmployee.GetEmployee();
            pnDanhSachNhanVien.Controls.Clear();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, employee);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
                pnThemNhanVien.Visible = false;
            }
        }
        // Load Data Employee Role
        void LoadDataEmployeeRole()
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = dbEmployee.GetEmployeeRole();
            pnDanhSachNhanVien.Controls.Clear();
            if (employees != null)
            {
                foreach (Employee employee in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, employee);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
                pnThemNhanVien.Visible = false;
            }
            cboChucVu.SelectedIndex = 1;
            cboChucVu.Enabled = false;
        }
        //Check Insert Employee
        public bool CheckEmployee()
        {
            if (txtUser.Text.Equals(""))
            {
                lbCheckUser.ForeColor = Color.Red;
                lbCheckUser.Text = "Tài khoản không được để trống";
                lbCheckUser.Visible = true;
                txtUser.Focus();
                return false;
            }
            if (txtPassword.Text.Equals(""))
            {
                lbCheckPass.ForeColor = Color.Red;
                lbCheckPass.Text = "Mật khẩu không được để trống";
                lbCheckPass.Visible = true;
                txtPassword.Focus();
                return false;
            }
            if (txtEmployeeName.Text.Equals(""))
            {
                lbCheckName.ForeColor = Color.Red;
                lbCheckName.Text = "Tên không được để trống";
                lbCheckName.Visible = true;
                txtEmployeeName.Focus();
                return false;
            }
            string[] name = txtEmployeeName.Text.Split(' ');
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = char.ToUpper(name[i][0]) + name[i].Substring(1).ToLower();
            }
            txtEmployeeName.Text = String.Join(" ", name);
            if (txtEmployeePhone.Text.Equals(""))
            {
                lbCheckPhone.ForeColor = Color.Red;
                lbCheckPhone.Text = "Số điện thoại không được để trống";
                lbCheckPhone.Visible = true;
                txtEmployeePhone.Focus();
                return false;
            }

            string phone = "";
            foreach (Char c in txtEmployeePhone.Text)
            {
                if (Char.IsDigit(c))
                {
                    phone += c;
                }
            }
            if (phone.Length != 10)
            {
                lbCheckPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                lbCheckPhone.ForeColor = Color.Red;
                lbCheckPhone.Visible = true;
                txtEmployeePhone.Focus();
                return false;
            }
            if (phone[0] != '0')
            {
                txtEmployeePhone.Focus();
                lbCheckPhone.ForeColor = Color.Red;
                lbCheckPhone.Visible = true;
                lbCheckPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                return false;
            }
            if (txtEmployeeAddress.Text.Equals(""))
            {
                lbCheckAdress.Text = "Địa chỉ không được để trống";
                lbCheckAdress.ForeColor = Color.Red;
                lbCheckAdress.Visible = true;
                txtEmployeeAddress.Focus();
                return false;
            }
            string[] address = txtEmployeeAddress.Text.Split(' ');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = char.ToUpper(address[i][0]) + address[i].Substring(1).ToLower();
            }
            txtEmployeeAddress.Text = String.Join(" ", address);
            if (txtEmployeeEmail.Text.Equals(""))
            {
                lbCheckEmail.Text = "email không được để trống";
                lbCheckEmail.ForeColor = Color.Red;
                lbCheckEmail.Visible = true;
                txtEmployeeEmail.Focus();
                return false;
            }
            try
            {
                MailAddress mailaddress = new MailAddress(txtEmployeeEmail.Text);
            }
            catch
            {
                lbCheckEmail.Text = "Vui lòng nhập đúng định dạng email";
                lbCheckEmail.ForeColor = Color.Red;
                lbCheckEmail.Visible = true;
                txtEmployeeEmail.Focus();
                return false;
            }
            return true;
        }

        // Exit Apllication
        private void ptbClose_Click(object sender, EventArgs e)
        {
            pnThemNhanVien.Visible = false;
            PhanQuyen();
        }
        void ResetTextEmployee()
        {
            txtUser.Text = "";
            txtPassword.Text = "";
            txtEmployeeName.Text = "";
            cboGender.SelectedIndex = 0;
            cboChucVu.SelectedIndex = 0;
            cboBirthday.Value = DateTime.Now;
            txtEmployeePhone.Text = "";
            txtEmployeeAddress.Text = "";
            txtEmployeeEmail.Text = "";
            ptbAnhDaiDien.Image = Image.FromFile(imageEmployee);
        }
        // Display panel Add Employee
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            ResetTextEmployee();
            pnThemNhanVien.BringToFront();
            pnThemNhanVien.Visible = true;
            btnUpdateEmployee.Visible = false;
            btnDeleteEmployee.Visible = false;
            btnChonAnh.Visible = false;
            btnEmployeeSave.Visible = true;
            btnAddimageEmployee.Visible = true;
        }
        // Get local Image Employee
        private void btnAddimageEmployee_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn ảnh";
            openFile.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Lọc file ảnh
            openFile.InitialDirectory = @"D:\images-NET"; // Thư mục mặc định khi mở
            openFile.Multiselect = false; // Chỉ cho phép chọn 1 ảnh
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imageEmployee = openFile.FileName; //Lấy đường dẫn của ảnh từ hộp thoại chọn file (openFile.FileName).
                ptbAnhDaiDien.Image = Image.FromFile(imageEmployee); // Tải ảnh từ đường dẫn đó vào PictureBox 
                ptbAnhDaiDien.SizeMode = PictureBoxSizeMode.StretchImage; //Điều chỉnh kích thước ảnh sao cho nó kéo giãn để lấp đầy toàn bộ diện tích của PictureBox (StretchImage).
            }
        }
        // Add Employee 
        private void btnEmployeeSave_Click(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();

            if (CheckEmployee())
            {
                if (dbEmployee.InsertEmployee(txtUser.Text,
                                              Password.HashPassword(txtPassword.Text),
                                              txtEmployeeName.Text,
                                              cboGender.SelectedItem.ToString(),
                                              cboChucVu.SelectedItem.ToString(),
                                              txtEmployeePhone.Text,
                                              cboBirthday.Value,
                                              txtEmployeeAddress.Text,
                                              txtEmployeeEmail.Text,
                                              imageEmployee))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo");
                    ResetTextEmployee();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Thông báo");
                }
            }
        }
        private void txtFindEmployee_TextChanged(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = new List<Employee>();
            if (cboFindEmployee.SelectedIndex == 0)
            {
                employees = dbEmployee.FindEmployeeName(txtFindEmployee.Text, employee.Role);
                pnDanhSachNhanVien.Controls.Clear();
                foreach (Employee emp in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, emp);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
            }
            else
            {
                employees = dbEmployee.FindEmployeeRole(txtFindEmployee.Text);
                pnDanhSachNhanVien.Controls.Clear();
                foreach (Employee emp in employees)
                {
                    DisplayEmployee displayEmployee = new DisplayEmployee(pnThemNhanVien, emp);
                    pnDanhSachNhanVien.Controls.Add(displayEmployee);
                }
                pnDanhSachNhanVien.Refresh();
            }
            if (string.IsNullOrEmpty(txtFindEmployee.Text))
            {
                PhanQuyen();
            }
        }
        #endregion
        #region UI Employee
        void DisplayPanel(Guna2Panel panel)
        {
            foreach (var item in this.Controls)
            {
                if (item is Guna2Panel)
                {
                    Guna2Panel p = (Guna2Panel)item;
                    if (p != pnFunction)
                    {
                        p.Visible = false;
                    }
                }
            }
            panel.Visible = true;
            panel.BringToFront();
        }
        void DisplayButton(Guna2Button button)
        {
            foreach (var item in pnFunction.Controls)
            {
                if (item is Guna2Button)
                {
                    Guna2Button b = (Guna2Button)item;
                    b.ForeColor = Color.White;
                    b.FillColor = Color.MediumSlateBlue;
                }

                button.ForeColor = Color.MediumSlateBlue;
                button.FillColor = Color.White;
            }
        }
        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            lbCheckUser.Visible = false;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lbCheckPass.Visible = false;
            txtPassword.PasswordChar = '*';
        }
        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar.Equals('*'))
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
        private void txtEmployeePhone_TextChanged(object sender, EventArgs e)
        {
            lbCheckPhone.Visible = false;
        }

        private void txtEmployeeAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckAdress.Visible = false;
        }

        private void txtEmployeeEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckEmail.Visible = false;
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLyNhanVien);
            DisplayButton(btnQuanLyNhanVien);
        }

        private void pnDisplayEmployee_Click(object sender, EventArgs e)
        {
            pnDanhSachNhanVien.Visible = false;
        }
        #endregion
        #region Trang chủ
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            DisplayButton(btnTrangChu);
            DisplayPanel(pnHome);
            DbEmployee dbEmployee = new DbEmployee();
            dbCustomer dbCustomer = new dbCustomer();
            dbSell dbSell = new dbSell();
            lbEmployeeQuantity.Text = "Số lượng nhân viên: " + dbEmployee.GetEmployeeCount();
            lbCustomerQuantity.Text = "Số lượng khách hàng: " + dbCustomer.GetCustomerCount();
            lbTotalDoanhThu.Text = "Doanh thu: " + dbSell.GetTotalRevenue() + "VND";
        }
        private void DisplayChart(DataSet ds)
        {

            // Kiểm tra dữ liệu trả về từ DataSet
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No data found.");
                return;
            }
            // Xóa các series cũ trong biểu đồ
            chart1.Series.Clear();
            chart1.Series.Add("Doanh thu");
            chart1.Series["Doanh thu"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DateTime date = Convert.ToDateTime(row["DateOut"]);
                decimal totalPrice = Convert.ToDecimal(row["TotalPrice"]);

                // Chuyển đổi ngày thành chuỗi với định dạng dd/MM/yyyy
                string formattedDate = date.ToString("dd/MM/yyyy");

                // Thêm điểm vào biểu đồ
                chart1.Series["Doanh thu"].Points.AddXY(formattedDate, totalPrice);
            }

            // Cập nhật lại biểu đồ
            chart1.Invalidate();
        }
        private void cboChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbSell dbSell = new dbSell();
            DataSet ds = new DataSet();

            if (cboChart.SelectedIndex == 0)
            {
                ds = dbSell.GetSellDateDay();
            }
            else if (cboChart.SelectedIndex == 1)
            {
                ds = dbSell.GetSellDate7Day();
            }
            else if (cboChart.SelectedIndex == 2)
            {
                ds = dbSell.GetSellDateMonth();
            }
            else
            {
                ds = dbSell.GetSellDateYear();
            }
            DisplayChart(ds);
        }

        #endregion
        #region Đăng xuất tài khoản
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DisplayButton(btnDangXuat);
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
                this.Close();
            }
            else
            {
                btnDangXuat.FillColor = Color.MediumSlateBlue;
                btnDangXuat.ForeColor = Color.White;
            }
        }

        #endregion
        #region Nhà cung cấp
        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLyNhaCungCap);
            DisplayButton(btnQuanLyNhaCungCap);
            btnSupplierSave.Enabled = false;
            LoadDataSupplier();
            dgvSupplier.ReadOnly = true;
        }
        // Reset text supplier
        void ResetTextSupplier()
        {
            txtSupplierID.Text = "";
            txtSupplierName.Text = "";
            txtSupplierAddress.Text = "";
            txtSupplierPhone.Text = "";
            txtSupplierEmail.Text = "";
        }
        // Check insert update supplier
        bool CheckSupplier()
        {
            if (string.IsNullOrEmpty(txtSupplierID.Text))
            {
                lbCheckSupplierID.Visible = true;
                lbCheckSupplierID.Text = "Vui lòng nhập ID";
                lbCheckSupplierID.ForeColor = Color.Red;
                return false;
            }
            if (txtSupplierName.Text.Equals(""))
            {
                lbCheckSupplierName.ForeColor = Color.Red;
                lbCheckSupplierName.Text = "Tên không được để trống";
                lbCheckSupplierName.Visible = true;
                txtSupplierName.Focus();
                return false;
            }
            string[] name = txtSupplierName.Text.Split(' ');
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = char.ToUpper(name[i][0]) + name[i].Substring(1).ToLower();
            }
            txtSupplierName.Text = String.Join(" ", name);
            if (txtSupplierPhone.Text.Equals(""))
            {
                lbCheckSupplierPhone.ForeColor = Color.Red;
                lbCheckSupplierPhone.Text = "Số điện thoại không được để trống";
                lbCheckSupplierPhone.Visible = true;
                txtSupplierPhone.Focus();
                return false;
            }

            string phone = "";
            foreach (Char c in txtSupplierPhone.Text)
            {
                if (Char.IsDigit(c))
                {
                    phone += c;
                }
            }
            if (phone.Length != 10)
            {
                lbCheckSupplierPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                lbCheckSupplierPhone.ForeColor = Color.Red;
                lbCheckSupplierPhone.Visible = true;
                txtSupplierPhone.Focus();
                return false;
            }
            if (phone[0] != '0')
            {
                txtSupplierPhone.Focus();
                lbCheckSupplierPhone.ForeColor = Color.Red;
                lbCheckSupplierPhone.Visible = true;
                lbCheckSupplierPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                return false;
            }
            if (txtSupplierAddress.Text.Equals(""))
            {
                lbCheckSupplierAddress.Text = "Địa chỉ không được để trống";
                lbCheckSupplierAddress.ForeColor = Color.Red;
                lbCheckSupplierAddress.Visible = true;
                txtSupplierAddress.Focus();
                return false;
            }
            string[] address = txtSupplierAddress.Text.Split(' ');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = char.ToUpper(address[i][0]) + address[i].Substring(1).ToLower();
            }
            txtSupplierAddress.Text = String.Join(" ", address);
            if (txtSupplierEmail.Text.Equals(""))
            {
                lbCheckSupplierEmail.Text = "email không được để trống";
                lbCheckSupplierEmail.ForeColor = Color.Red;
                lbCheckSupplierEmail.Visible = true;
                txtSupplierEmail.Focus();
                return false;
            }
            try
            {
                MailAddress mailaddress = new MailAddress(txtSupplierEmail.Text);
            }
            catch
            {
                lbCheckSupplierEmail.Text = "Vui lòng nhập đúng định dạng email";
                lbCheckSupplierEmail.ForeColor = Color.Red;
                lbCheckSupplierEmail.Visible = true;
                txtSupplierEmail.Focus();
                return false;
            }
            return true;
        }
        // Load Data Supplier in Database
        void LoadDataSupplier()
        {
            dbSupplier dbSupplier = new dbSupplier();
            DataSet ds = dbSupplier.GetSupplier();
            dgvSupplier.DataSource = null;
            dgvSupplier.DataSource = ds.Tables[0];
        }
        private void btnSupplierAdd_Click(object sender, EventArgs e)
        {
            ResetTextSupplier();
            btnSupplierUpdate.Enabled = false;
            btnSupplierAdd.Enabled = false;
            btnSupplierDelete.Enabled = false;
            btnSupplierSave.Enabled = true;
            txtSupplierID.Enabled = true;
        }
        #region Thêm nhà cung cấp
        private void btnSupplierSave_Click(object sender, EventArgs e)
        {
            dbSupplier dbSupp = new dbSupplier();
            DataSet ds = dbSupp.GetSupplier();
            dgvSupplier.DataSource = null;
            dgvSupplier.DataSource = ds.Tables[0];
            if (CheckSupplier())
            {
                if (ds.Tables[0].Rows.Count < 0)
                {
                    dbSupplier dbSupplier = new dbSupplier();
                    dbSupplier.InsertSupplier(txtSupplierID.Text, txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierPhone.Text, txtSupplierEmail.Text);
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo");
                    ResetTextSupplier();
                    LoadDataSupplier();
                }
                else
                {
                    foreach (DataGridViewRow row in dgvSupplier.Rows)
                    {
                        if (txtSupplierID.Text.Equals(row.Cells["SupplierID"].Value?.ToString()))
                        {
                            MessageBox.Show("Đã có ID này!", "Thông báo");
                            return; // Thoát nếu phát hiện ID đã tồn tại
                        }
                    }
                    // Thêm nhà cung cấp mới nếu không trùng ID
                    dbSupplier dbSupplier = new dbSupplier();
                    dbSupplier.InsertSupplier(txtSupplierID.Text, txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierPhone.Text, txtSupplierEmail.Text);
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo");
                    ResetTextSupplier();
                    LoadDataSupplier();
                }
            }
        }
        #endregion
        private void txtSupplierID_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierID.Visible = false;
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierName.Visible = false;
        }

        private void txtSupplierPhone_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierPhone.Visible = false;
        }

        private void txtSupplierEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierEmail.Visible = false;
        }

        private void txtSupplierAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierAddress.Visible = false;
        }

        private void btnSupplierCancel_Click(object sender, EventArgs e)
        {
            btnSupplierAdd.Enabled = true;
            btnSupplierUpdate.Enabled = true;
            btnSupplierDelete.Enabled = true;
            ResetTextSupplier();
            txtSupplierID.Enabled = true;
        }

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSupplierID.Text = dgvSupplier.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtSupplierName.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSupplierAddress.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSupplierPhone.Text = dgvSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSupplierEmail.Text = dgvSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtSupplierID.Enabled = false;
            }
        }
        #region Cập nhật nhà cung cấp
        private void btnSupplierUpdate_Click(object sender, EventArgs e)
        {
            dbSupplier dbSupplier = new dbSupplier();
            if (txtSupplierID.Enabled == false)
            {
                if (CheckSupplier())
                {
                    dbSupplier.UpdateSupplier(txtSupplierID.Text, txtSupplierName.Text, txtSupplierAddress.Text, txtSupplierPhone.Text, txtSupplierEmail.Text);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    LoadDataSupplier();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật", "Thông báo");
            }
        }
        #endregion
        #region Xóa nhà cung cấp
        private void btnSupplierDelete_Click(object sender, EventArgs e)
        {
            dbSupplier dbSupplier = new dbSupplier();
            if (txtSupplierID.Enabled == false)
            {
                if (MessageBox.Show("Bạn có muốn xóa nhà cung cấp này không!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbSupplier.DeleteSupplier(txtSupplierID.Text);
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDataSupplier();
                    ResetTextSupplier();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo");
            }
        }
        #endregion

        #region Tìm kiếm nhà cung cấp
        private void txtSupplierFind_TextChanged(object sender, EventArgs e)
        {
            dbSupplier dbSupplier = new dbSupplier();
            if (cboFindSupplier.SelectedIndex == 0)
            {
                DataSet ds = dbSupplier.FindSupplierID(txtSupplierFind.Text);
                dgvSupplier.DataSource = null;
                dgvSupplier.DataSource = ds.Tables[0];
            }
            else
            {
                DataSet ds = dbSupplier.FindSupplierName(txtSupplierFind.Text);
                dgvSupplier.DataSource = null;
                dgvSupplier.DataSource = ds.Tables[0];
            }
            if (string.IsNullOrEmpty(txtSupplierFind.Text))
            {
                LoadDataSupplier();
            }
        }
        #endregion
        #endregion
        #region Khách hàng
        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLyKhachHang);
            DisplayButton(btnQuanLyKhachHang);
            btnCustormerSave.Enabled = false;
            LoadDataCustomer();
            dgvCustomer.ReadOnly = true;
        }
        // Reset text supplier
        void ResetTextCustomer()
        {
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtCustomerAddress.Text = "";
            txtCustomerPhone.Text = "";
            txtCustomerEmail.Text = "";
        }
        // Check insert update supplier
        bool CheckCustomer()
        {
            if (string.IsNullOrEmpty(txtCustomerID.Text))
            {
                lbCheckCustomerID.Visible = true;
                lbCheckCustomerID.Text = "Vui lòng nhập ID";
                lbCheckCustomerID.ForeColor = Color.Red;
                return false;
            }
            if (txtCustomerName.Text.Equals(""))
            {
                lbCheckCustomerName.ForeColor = Color.Red;
                lbCheckCustomerName.Text = "Tên không được để trống";
                lbCheckCustomerName.Visible = true;
                txtCustomerName.Focus();
                return false;
            }
            string[] name = txtCustomerName.Text.Split(' ');
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = char.ToUpper(name[i][0]) + name[i].Substring(1).ToLower();
            }
            txtCustomerName.Text = String.Join(" ", name);
            if (txtCustomerPhone.Text.Equals(""))
            {
                lbCheckCustomerPhone.ForeColor = Color.Red;
                lbCheckCustomerPhone.Text = "Số điện thoại không được để trống";
                lbCheckCustomerPhone.Visible = true;
                txtCustomerPhone.Focus();
                return false;
            }

            string phone = "";
            foreach (Char c in txtCustomerPhone.Text)
            {
                if (Char.IsDigit(c))
                {
                    phone += c;
                }
            }
            if (phone.Length != 10)
            {
                lbCheckCustomerPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                lbCheckCustomerPhone.ForeColor = Color.Red;
                lbCheckCustomerPhone.Visible = true;
                txtCustomerPhone.Focus();
                return false;
            }
            if (phone[0] != '0')
            {
                txtCustomerPhone.Focus();
                lbCheckCustomerPhone.ForeColor = Color.Red;
                lbCheckCustomerPhone.Visible = true;
                lbCheckCustomerPhone.Text = "Vui lòng nhập đúng định dạng số điện thoại";
                return false;
            }
            if (txtCustomerAddress.Text.Equals(""))
            {
                lbCheckCustomerAddress.Text = "Địa chỉ không được để trống";
                lbCheckCustomerAddress.ForeColor = Color.Red;
                lbCheckCustomerAddress.Visible = true;
                txtCustomerAddress.Focus();
                return false;
            }
            string[] address = txtCustomerAddress.Text.Split(' ');
            for (int i = 0; i < address.Length; i++)
            {
                address[i] = char.ToUpper(address[i][0]) + address[i].Substring(1).ToLower();
            }
            txtCustomerAddress.Text = String.Join(" ", address);
            if (txtCustomerEmail.Text.Equals(""))
            {
                lbCheckCustomerEmail.Text = "email không được để trống";
                lbCheckCustomerEmail.ForeColor = Color.Red;
                lbCheckCustomerEmail.Visible = true;
                txtCustomerEmail.Focus();
                return false;
            }
            try
            {
                MailAddress mailaddress = new MailAddress(txtCustomerEmail.Text);
            }
            catch
            {
                lbCheckCustomerEmail.Text = "Vui lòng nhập đúng định dạng email";
                lbCheckCustomerEmail.ForeColor = Color.Red;
                lbCheckCustomerEmail.Visible = true;
                txtCustomerEmail.Focus();
                return false;
            }
            return true;
        }
        // Load Data Supplier in Database
        void LoadDataCustomer()
        {
            dbCustomer dbCustomer = new dbCustomer();
            DataSet ds = dbCustomer.GetCustomer();
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = ds.Tables[0];
        }

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            ResetTextSupplier();
            btnCustomerUpdate.Enabled = false;
            btnCustomerAdd.Enabled = false;
            btnCustomerDelete.Enabled = false;
            btnCustormerSave.Enabled = true;
            txtCustomerID.Enabled = true;
        }

        private void btnCustormerSave_Click(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            DataSet ds = dbCustomer.GetCustomer();
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = ds.Tables[0];
            if (CheckCustomer())
            {
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    dbCustomer dbCusto = new dbCustomer();
                    dbCusto.InsertCustomer(txtCustomerID.Text, txtCustomerName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                    ResetTextCustomer();
                    LoadDataCustomer(); // Tải lại dữ liệu sau khi chèn
                    return;
                }
                else
                {
                    foreach (DataGridViewRow row in dgvCustomer.Rows)
                    {
                        if (txtCustomerID.Text.Equals(row.Cells["CustomerID"].Value?.ToString()))
                        {
                            MessageBox.Show("Đã có ID này!", "Thông báo");
                            return; // Thoát nếu phát hiện ID đã tồn tại
                        }
                    }
                    // Thêm nhà cung cấp mới nếu không trùng ID
                    dbCustomer dbCus = new dbCustomer();
                    dbCus.InsertCustomer(txtCustomerID.Text, txtCustomerName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                    ResetTextCustomer();
                    LoadDataCustomer();
                }
            }
        }

        private void txtCustomerID_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerID.Visible = false;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerName.Visible = false;
        }

        private void txtCustomerPhone_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerPhone.Visible = false;
        }

        private void txtCustomerEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerEmail.Visible = false;
        }

        private void txtCustomerAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckCustomerAddress.Visible = false;
        }

        private void btnCustomerCancel_Click(object sender, EventArgs e)
        {
            btnCustomerAdd.Enabled = true;
            btnCustomerUpdate.Enabled = true;
            btnCustomerDelete.Enabled = true;
            ResetTextCustomer();
            txtCustomerID.Enabled = true;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCustomerID.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCustomerName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCustomerAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCustomerPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCustomerEmail.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtCustomerID.Enabled = false;
            }
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            if (txtCustomerID.Enabled == false)
            {
                if (CheckCustomer())
                {
                    dbCustomer.UpdateCustomer(txtCustomerID.Text, txtCustomerName.Text, txtCustomerAddress.Text, txtCustomerPhone.Text, txtCustomerEmail.Text);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    LoadDataCustomer();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật", "Thông báo");
            }
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();
            if (txtCustomerID.Enabled == false)
            {
                if (MessageBox.Show("Bạn có muốn xóa khách hàng này không!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbCustomer.DeleteCustomer(txtCustomerID.Text);
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDataCustomer();
                    ResetTextCustomer();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa!", "Thông báo");
            }
        }

        private void txtCustomerFind_TextChanged(object sender, EventArgs e)
        {
            dbCustomer dbCustomer = new dbCustomer();

            if (cboCustomerFind.SelectedIndex == 0)
            {
                DataSet ds = dbCustomer.FindCustomerID(txtCustomerFind.Text);
                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = ds.Tables[0];
            }
            else
            {
                DataSet ds = dbCustomer.FindCustomerName(txtCustomerFind.Text);
                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = ds.Tables[0];
            }
            if (string.IsNullOrEmpty(txtCustomerFind.Text))
            {
                LoadDataCustomer();
            }
        }
        #endregion
        #region Sản phẩm
        void LoadDataProduct()
        {
            dbProduct dbProduct = new dbProduct();
            DataSet ds = dbProduct.GetProduct();
            pnDanhSachSanPham.Controls.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                pnDanhSachSanPham.Controls.Add(displayProduct);
            }
            pnDanhSachSanPham.Refresh();
        }

        bool CheckProduct()
        {
            if (string.IsNullOrEmpty(txtProductID.Text))
            {
                lbCheckProductID.Text = "Vui lòng nhập mã";
                lbCheckProductID.ForeColor = Color.Red;
                lbCheckProductID.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                lbCheckProductName.Text = "Vui lòng nhập tên";
                lbCheckProductName.ForeColor = Color.Red;
                lbCheckProductName.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductPrice.Text))
            {
                lbCheckProductPrice.Text = "Vui lòng nhập giá";
                lbCheckProductPrice.ForeColor = Color.Red;
                lbCheckProductPrice.Visible = true;
                return false;
            }
            if (!decimal.TryParse(txtProductPrice.Text, out decimal price))
            {
                lbCheckProductPrice.Text = "Vui lòng nhập đúng số";
                lbCheckProductPrice.ForeColor = Color.Red;
                lbCheckProductPrice.Visible = true;
                return false;
            }
            if (price < 0)
            {
                lbCheckProductPrice.Text = "Vui lòng nhập số dương";
                lbCheckProductPrice.ForeColor = Color.Red;
                lbCheckProductPrice.Visible = true;
                return false;
            }
            return true;
        }
        void ResetTextProduct()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            ptbImageProduct.Image = Image.FromFile("D:\\images-NET\\product.jpg");
            cboProductCategory.StartIndex = 0;
            cboProductSupplier.StartIndex = 0;
            cboProductStatus.StartIndex = 1;
            txtProductDescription.Text = "";
        }
        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            pnTaoSanPham.BringToFront();
            pnTaoSanPham.Visible = true;
            btnChonAnhSanPham.Visible = false;
            btnProductUpdate.Visible = false;
            btnProductDelete.Visible = false;
            ResetTextProduct();
            txtProductID.Enabled = true;
            btnProductSave.Visible = true;
            btnThemAnhSanPham.Visible = true;
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnQuanLySanPham);
            DisplayButton(btnQuanLySanPham);

            // Tải danh mục sản phẩm
            dbCategories dbCategories = new dbCategories();
            DataSet dsCategory = dbCategories.GetCategory();

            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                cboProductCategory.DataSource = dsCategory.Tables[0]; // gán bảng dữ liệu cho combobox
                cboProductCategory.DisplayMember = "CategoryName"; // Hiển thị tên danh mục
                cboProductCategory.ValueMember = "CategoryID"; // Lưu ID danh mục , Xác định cột nào trong DataSource được sử dụng làm giá trị bên trong khi lựa chọn một mục.
                cboProductCategory.SelectedValue = dsCategory.Tables[0].Rows[0]["CategoryID"]; // Gán giá trị mặc định
                //Lấy giá trị CategoryID từ dòng đầu tiên của bảng danh mục(dsCategory.Tables[0]).
                //ComboBox tự động chọn mục có CategoryID khớp với giá trị được gán.
            }
            else
            {
                MessageBox.Show("Không có danh mục sản phẩm nào. Vui lòng thêm danh mục trước.", "Thông báo");
                cboProductCategory.DataSource = null; // Đặt về null nếu không có dữ liệu
            }

            // Tải nhà cung cấp
            dbSupplier dbSupplier = new dbSupplier();
            DataSet dsSupplier = dbSupplier.GetSupplier();

            if (dsSupplier != null && dsSupplier.Tables.Count > 0 && dsSupplier.Tables[0].Rows.Count > 0)
            {
                cboProductSupplier.DataSource = dsSupplier.Tables[0];
                cboProductSupplier.DisplayMember = "SupplierName"; // Hiển thị tên nhà cung cấp
                cboProductSupplier.ValueMember = "SupplierID"; // Lưu ID nhà cung cấp
                cboProductSupplier.SelectedValue = dsSupplier.Tables[0].Rows[0]["SupplierID"]; // Gán giá trị mặc định
            }
            else
            {
                MessageBox.Show("Không có nhà cung cấp nào. Vui lòng thêm nhà cung cấp trước.", "Thông báo");
                cboProductSupplier.DataSource = null; // Đặt về null nếu không có dữ liệu
            }

            Console.WriteLine("CategoryID: " + cboProductCategory.SelectedValue);
            Console.WriteLine("SupplierID: " + cboProductSupplier.SelectedValue);
            LoadDataProduct();
        }


        private void ptbProductClose_Click(object sender, EventArgs e)
        {
            pnTaoSanPham.Visible = false;
            LoadDataProduct();
            ResetTextProduct();
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if (CheckProduct())
            {
                dbProduct dbProduct = new dbProduct();
                DataSet ds = dbProduct.GetProduct();
                if (ds.Tables[0].Rows.Count < 0)
                {
                    dbProduct.InsertProduct(txtProductID.Text, txtProductName.Text,
                        int.Parse(txtProductQuantity.Text), cboProductCategory.SelectedValue.ToString(),
                        cboProductSupplier.SelectedValue.ToString(), decimal.Parse(txtProductPrice.Text),
                        cboProductStatus.SelectedItem.ToString(), txtProductDescription.Text, imageProduct);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["ProductID"].ToString().Equals(txtProductID.Text))
                        {
                            MessageBox.Show("Đã có ID này", "Thông báo");
                            return;
                        }
                    }
                    dbProduct.InsertProduct(txtProductID.Text, txtProductName.Text,
                        int.Parse(txtProductQuantity.Text), cboProductCategory.SelectedValue.ToString(),
                        cboProductSupplier.SelectedValue.ToString(), decimal.Parse(txtProductPrice.Text),
                        cboProductStatus.SelectedItem.ToString(), txtProductDescription.Text, imageProduct);
                    MessageBox.Show("Tạo mới sản phẩm thành công!", "Thông báo");
                }
            }
        }

        private void btnThemAnhSanPham_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn ảnh";
            openFile.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Lọc file ảnh
            openFile.InitialDirectory = @"D:\images-NET"; // Thư mục mặc định khi mở
            openFile.Multiselect = false; // Chỉ cho phép chọn 1 ảnh
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imageProduct = openFile.FileName; //Lấy đường dẫn của ảnh từ hộp thoại chọn file (openFile.FileName).
                ptbImageProduct.Image = Image.FromFile(imageProduct); // Tải ảnh từ đường dẫn đó vào PictureBox 
                ptbImageProduct.SizeMode = PictureBoxSizeMode.StretchImage; //Điều chỉnh kích thước ảnh sao cho nó kéo giãn để lấp đầy toàn bộ diện tích của PictureBox (StretchImage).
            }
        }

        private void txtProductID_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductID.Visible = false;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductName.Visible = false;
        }

        private void txtProductPrice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductPrice.Visible = false;
        }

        private void cboFilterProductStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbProduct dbProduct = new dbProduct();

            if (cboFilterProductStatus.SelectedIndex == 0)
            {
                LoadDataProduct();
            }
            else if (cboFilterProductStatus.SelectedIndex == 1)
            {
                DataSet ds = dbProduct.GetProductStatus();
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
            else
            {
                DataSet ds = dbProduct.GetProductStatus_SELL();
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
        }

        private void txtFindProduct_TextChanged(object sender, EventArgs e)
        {
            dbProduct dbProduct = new dbProduct();
            if (cboProductFind.SelectedIndex == 0)
            {
                DataSet ds = dbProduct.GetProductID(txtFindProduct.Text);
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
            else
            {
                DataSet ds = dbProduct.GetProductName(txtFindProduct.Text);
                pnDanhSachSanPham.Controls.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DisplayProduct displayProduct = new DisplayProduct(dr, pnTaoSanPham);
                    pnDanhSachSanPham.Controls.Add(displayProduct);
                }
                pnDanhSachSanPham.Refresh();
            }
            if (string.IsNullOrEmpty(txtFindProduct.Text))
            {
                LoadDataProduct();
            }
        }
        #endregion
        #region Nhập sản phẩm
        bool CheckAddProduct()
        {
            if (string.IsNullOrEmpty(txtPurchaseInvoiceID.Text))
            {
                lbCheckPurchaseInvoiceID.Visible = true;
                lbCheckPurchaseInvoiceID.ForeColor = Color.Red;
                lbCheckPurchaseInvoiceID.Text = "Vui lòng nhập mã hóa đơn";
                return false;
            }
            if (!int.TryParse(txtProductQuantityInvoice.Text, out int quatity))
            {
                lbCheckProductInvoice.Text = "Vui lòng nhập đúng số";
                lbCheckProductInvoice.ForeColor = Color.Red;
                lbCheckProductInvoice.Visible = true;
                return false;
            }
            if (quatity <= 0)
            {
                lbCheckProductInvoice.Text = "Vui lòng nhập số dương";
                lbCheckProductInvoice.ForeColor = Color.Red;
                lbCheckProductInvoice.Visible = true;
                return false;
            }
            if (!decimal.TryParse(txtProductUnitPrice.Text, out decimal price))
            {
                lbCheckProductUnitPrice.Text = "Vui lòng nhập đúng số";
                lbCheckProductUnitPrice.ForeColor = Color.Red;
                lbCheckProductUnitPrice.Visible = true;
                return false;
            }
            if (price <= 0)
            {
                lbCheckProductUnitPrice.Text = "Vui lòng nhập số dương";
                lbCheckProductUnitPrice.ForeColor = Color.Red;
                lbCheckProductUnitPrice.Visible = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductNameInvoice.Text))
            {
                lbCheckProductNameInvoice.Text = "Vui lòng chọn sản phẩm";
                lbCheckProductNameInvoice.ForeColor = Color.Red;
                lbCheckProductNameInvoice.Visible = true;
                return false;
            }
            return true;

        }
        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hủy hóa đơn nhập sản phẩm?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pnNhapSanPham.Visible = false;
            }
        }

        void ResetHoaDonNhap()
        {
            txtPurchaseInvoiceID.Text = "";
            txtProductQuantityInvoice.Text = "";
            txtProductUnitPrice.Text = "";
            dgvProductCart.Rows.Clear();
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (CheckAddProduct())
            {
                // Lấy dòng được chọn từ ComboBox (là DataRowView)
                DataRowView selectedRow = (DataRowView)cboProductID.SelectedItem;

                // Truy cập giá trị ProductID từ DataRowView
                string productID = selectedRow["ProductID"].ToString();

                // Tạo đối tượng Product từ các TextBox
                Product product = new Product(
                    productID,
                    txtProductNameInvoice.Text,
                    txtProductCategoryName.Text,
                    txtProductSupplierName.Text,
                    Convert.ToDecimal(txtProductUnitPrice.Text),
                    int.Parse(txtProductQuantityInvoice.Text)
                );

                // Kiểm tra nếu sản phẩm đã tồn tại trong DataGridView
                bool productExists = false;
                foreach (DataGridViewRow row in dgvProductCart.Rows)
                {
                    if (row.Cells["ProductID"].Value != null && row.Cells["ProductID"].Value.ToString() == product.ProductID)
                    {
                        // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                        int currentQuantity = Convert.ToInt32(row.Cells["ProductQuantity"].Value);
                        int newQuantity = currentQuantity + product.ProductQuantity;
                        row.Cells["ProductQuantity"].Value = newQuantity; // Cập nhật số lượng
                        row.Cells["GetTotalPrice"].Value = newQuantity * Convert.ToDecimal(row.Cells["ProductPrice"].Value); // Cập nhật giá trị tổng
                        productExists = true;
                        break;
                    }
                }

                // Nếu sản phẩm chưa tồn tại trong DataGridView, thêm sản phẩm mới
                if (!productExists)
                {
                    dgvProductCart.Rows.Add(product.ProductID, product.ProductName, product.CategoryName, product.SupplierName, product.ProductPrice, product.ProductQuantity, product.GetTotalPrice());
                }
            }
        }

        private void btnNhapSanPham_Click(object sender, EventArgs e)
        {
            DisplayButton(btnNhapSanPham);
            DisplayPanel(pnDanhSachHoaDonNhap);
            LoadDataPurchaseInvoice();
        }
        void LoadDataPurchaseInvoice()
        {
            dbPurchaseInvoice dbPurchaseInvoice = new dbPurchaseInvoice();
            DataSet ds = dbPurchaseInvoice.GetPurchaseInvoice();
            dgvPurchaseInvoice.DataSource = null;
            dgvPurchaseInvoice.DataSource = ds.Tables[0];
        }
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            pnNhapSanPham.Visible = true;
            pnNhapSanPham.BringToFront();
            dbProduct dbProduct = new dbProduct();
            DataSet ds = dbProduct.GetProductCategoySupplier();
            cboProductID.DataSource = null;
            cboProductID.DataSource = ds.Tables[0];
            cboProductID.DisplayMember = "ProductID";
            cboProductID.ValueMember = "ProductID";
            ResetHoaDonNhap();
        }

        private void txtPurchaseInvoiceID_TextChanged(object sender, EventArgs e)
        {
            lbCheckPurchaseInvoiceID.Visible = false;
        }

        private void txtProductQuantityInvoice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductInvoice.Visible = false;
        }

        private void txtProductUnitPrice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductUnitPrice.Visible = false;
        }

        private void txtProductNameInvoice_TextChanged(object sender, EventArgs e)
        {
            lbCheckProductNameInvoice.Visible = false;
        }
        private void cboProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox có dữ liệu hay không
            if (cboProductID.SelectedIndex != -1 && cboProductID.DataSource != null)
            {
                // Lấy DataTable từ DataSet
                DataTable dt = (DataTable)cboProductID.DataSource;

                // Lấy chỉ số của mục được chọn
                int selectedIndex = cboProductID.SelectedIndex;

                // Kiểm tra chỉ số hợp lệ
                if (selectedIndex >= 0 && selectedIndex < dt.Rows.Count)
                {
                    // Lấy DataRow tương ứng
                    DataRow selectedRow = dt.Rows[selectedIndex];

                    // Gán giá trị vào các TextBox
                    txtProductNameInvoice.Text = selectedRow["ProductName"].ToString();
                    txtProductCategoryName.Text = selectedRow["CategoryName"].ToString();
                    txtProductSupplierName.Text = selectedRow["SupplierName"].ToString();
                    ptbProductImageInvoice.Image = Image.FromFile(selectedRow["ProductImage"].ToString());
                }
            }
        }

        private void dgvProductCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductCart.Rows.Count > 0)
            {
                cboProductID.SelectedValue = dgvProductCart.Rows[e.RowIndex].Cells[0].Value;
                txtProductQuantityInvoice.Text = dgvProductCart.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtProductUnitPrice.Text = dgvProductCart.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox hoặc ComboBox
            // Lấy dòng được chọn từ ComboBox (là DataRowView)
            DataRowView selectedRow = (DataRowView)cboProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();
            int quantity = Convert.ToInt32(txtProductQuantityInvoice.Text); // Lấy số lượng từ TextBox
            decimal unitPrice = Convert.ToDecimal(txtProductUnitPrice.Text); // Lấy giá từ TextBox

            // Tìm hàng trong DataGridView có ID sản phẩm trùng với productID
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (row.Cells["ProductID"].Value.ToString() == productID)
                {
                    // Cập nhật giá trị trong các ô tương ứng của dòng đó
                    row.Cells["ProductQuantity"].Value = quantity;
                    row.Cells["ProductPrice"].Value = unitPrice;
                    row.Cells["GetTotalPrice"].Value = unitPrice * quantity;
                    Console.WriteLine("GetToTalPrice:" + row.Cells["GetTotalPrice"].Value);
                    break; // Dừng vòng lặp sau khi tìm thấy dòng cần cập nhật
                }
            }

            // Cập nhật lại DataGridView (nếu cần thiết)
            dgvProductCart.Refresh();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ComboBox (DataRowView)
            DataRowView selectedRow = (DataRowView)cboProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();

            // Duyệt qua các dòng trong DataGridView để tìm dòng có ProductID cần xóa
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (row.Cells["ProductID"].Value.ToString() == productID)
                {
                    // Xóa dòng tương ứng
                    dgvProductCart.Rows.RemoveAt(row.Index);
                    break; // Dừng vòng lặp sau khi xóa dòng cần tìm
                }
            }

            // Cập nhật lại DataGridView (nếu cần thiết)
            dgvProductCart.Refresh();
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            dbPurchaseInvoice dbPurchaseInvoice = new dbPurchaseInvoice();
            dbInvoiceDetails dbInvoiceDetails = new dbInvoiceDetails();
            dbProduct dbProduct = new dbProduct();
            Product product = new Product();
            DataSet ds = dbProduct.GetProduct();
            DataSet dataSet = dbPurchaseInvoice.GetPurchaseInvoice();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
            }
            decimal totalPrice = 0;
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua hàng trống (nếu có)
                {
                    var cellValue = row.Cells["GetTotalPrice"].Value;
                    // Chuyển đổi sang decimal
                    totalPrice += decimal.Parse(cellValue.ToString());
                }
            }
            // thêm hóa đơn nhập sản phẩm 
            dbPurchaseInvoice.InsertPurchaseInvoice(txtPurchaseInvoiceID.Text, employee.Employeeid, datePurchaseInvoice.Value, totalPrice);
            foreach (DataGridViewRow row in dgvProductCart.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow foundRow = null;
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        if (dataRow["ProductID"].ToString().Equals(row.Cells["ProductID"].Value.ToString()))
                        {
                            foundRow = dataRow;
                            break;
                        }
                    }
                    if (foundRow != null)
                    {
                        product.ProductID = foundRow["ProductID"].ToString();
                        product.ProductQuantity = int.Parse(foundRow["ProductQuantity"].ToString()) + int.Parse(row.Cells["ProductQuantity"].Value.ToString());
                        dbProduct.UpdateProductQuantity(row.Cells["ProductID"].Value.ToString(), product.ProductQuantity);
                    }
                    dbInvoiceDetails.InsertInvoiceDetail(txtPurchaseInvoiceID.Text, row.Cells["ProductID"].Value.ToString(), int.Parse(row.Cells["ProductQuantity"].Value.ToString()), decimal.Parse(row.Cells["ProductPrice"].Value.ToString()));

                }
            }
            MessageBox.Show("Nhập sản phẩm thành công!", "Thông báo");
        }

        private void dgvPurchaseInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPurchaseInvoice.Rows.Count > 0)
            {
                pnHienThiChiTietHoaDonNhap.BringToFront();
                pnHienThiChiTietHoaDonNhap.Visible = true;
                dbInvoiceDetails dbInvoiceDetails = new dbInvoiceDetails();

                DataSet ds = dbInvoiceDetails.GetPurchaseInvoiceDetail(dgvPurchaseInvoice.Rows[e.RowIndex].Cells[0].Value.ToString());
                dgvHienThiChiTietHoaDonNhap.DataSource = null;
                dgvHienThiChiTietHoaDonNhap.DataSource = ds.Tables[0];
                txtMaHDNhap.Text = dgvPurchaseInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void ptbHide_Click(object sender, EventArgs e)
        {
            pnNhapSanPham.Visible = false;
            LoadDataPurchaseInvoice();
        }

        private void ptbHidepnInvoiceDetail_Click(object sender, EventArgs e)
        {
            pnHienThiChiTietHoaDonNhap.Visible = false;
            LoadDataPurchaseInvoice();
        }
        private void btnXoaHoaDonNhap_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbPurchaseInvoice dbPurchaseInvoice = new dbPurchaseInvoice();
                dbPurchaseInvoice.DeletePurrchaseInvoice(txtMaHDNhap.Text);
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo");
                pnHienThiChiTietHoaDonNhap.Visible = false;
                LoadDataPurchaseInvoice();
            }
        }

        private void txtFindPurchaseInvoice_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFindPurchaseInvoice.Text))
            {
                LoadDataPurchaseInvoice();
                return;
            }

            dbPurchaseInvoice dbPurchaseInvoice = new dbPurchaseInvoice();

            if (cboPurchaseInvoice.SelectedIndex == 0)
            {
                // Tìm theo mã hóa đơn
                DataSet ds = dbPurchaseInvoice.GetPurchaseInvoiceID(txtFindPurchaseInvoice.Text);
                dgvPurchaseInvoice.DataSource = null;
                dgvPurchaseInvoice.DataSource = ds.Tables[0];
            }
            else if (cboPurchaseInvoice.SelectedIndex == 1)
            {
                // Tìm theo ngày hóa đơn
                string input = txtFindPurchaseInvoice.Text.Trim();

                // Không cần chuyển đổi sang DateTime, xử lý tìm kiếm chuỗi
                DataSet ds = dbPurchaseInvoice.GetPurchaseInvoiceDate(input);
                dgvPurchaseInvoice.DataSource = null;
                dgvPurchaseInvoice.DataSource = ds.Tables[0];
            }
        }
        #endregion
        #region Bán hàng
        bool CheckAddShoppingCart()
        {
            dbProduct dbProduct = new dbProduct();
            DataSet ds = dbProduct.GetProductCategoySupplierStatus();

            if (string.IsNullOrEmpty(txtSellID.Text))
            {
                lbCheckSellID.Visible = true;
                lbCheckSellID.ForeColor = Color.Red;
                lbCheckSellID.Text = "Vui lòng nhập mã hóa đơn";
                return false;
            }
            if (!int.TryParse(txtSellQuantity.Text, out int quatity))
            {
                lbCheckSellQuantity.Text = "Vui lòng nhập đúng số";
                lbCheckSellQuantity.ForeColor = Color.Red;
                lbCheckSellQuantity.Visible = true;
                return false;
            }
            if (quatity < 0)
            {
                lbCheckSellQuantity.Text = "Vui lòng nhập số dương";
                lbCheckSellQuantity.ForeColor = Color.Red;
                lbCheckSellQuantity.Visible = true;
                return false;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (quatity > int.Parse(row["ProductQuantity"].ToString()))
                {
                    lbCheckSellQuantity.Text = "Không đủ số lượng sản phẩm trong kho";
                    lbCheckSellQuantity.ForeColor = Color.Red;
                    lbCheckSellQuantity.Visible = true;
                    return false;
                }
            }
            return true;

        }
        void LoadDataSell()
        {
            dbSell dbSell = new dbSell();
            DataSet ds = dbSell.GetSell();
            dgvDanhSachHoaDonBan.DataSource = null;
            dgvDanhSachHoaDonBan.DataSource = ds.Tables[0];
        }
        private void btnBanHang_Click(object sender, EventArgs e)
        {
            DisplayPanel(pnBanHang);
            DisplayButton(btnBanHang);
            LoadDataSell();
        }

        void ResetHoaDonBan()
        {
            txtSellID.Text = "";
            txtSellQuantity.Text = "";
            txtSellPrice.Text = "";
            dgvShoppingCart.Rows.Clear();
        }
        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            pnHoaDonBanHang.Visible = true;
            pnHoaDonBanHang.BringToFront();
            ResetHoaDonBan();
            dbProduct dbProduct = new dbProduct();
            DataSet ds = dbProduct.GetProductCategoySupplierStatus();
            cboSellProductID.DataSource = null;
            cboSellProductID.DataSource = ds.Tables[0];
            cboSellProductID.DisplayMember = "ProductID";
            cboSellProductID.ValueMember = "ProductID";
            dbCustomer dbCustomer = new dbCustomer();
            DataSet data = dbCustomer.GetCustomer();
            cboSellCustomerID.DataSource = null;
            cboSellCustomerID.DataSource = data.Tables[0];
            cboSellCustomerID.DisplayMember = "CustomerID";
            cboSellCustomerID.ValueMember = "CustomerID";

        }

        private void cboSellProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox có dữ liệu hay không
            if (cboSellProductID.SelectedIndex != -1 && cboSellProductID.DataSource != null)
            {
                // Lấy DataTable từ DataSet
                DataTable dt = (DataTable)cboSellProductID.DataSource;

                // Lấy chỉ số của mục được chọn
                int selectedIndex = cboSellProductID.SelectedIndex;

                // Kiểm tra chỉ số hợp lệ
                if (selectedIndex >= 0 && selectedIndex < dt.Rows.Count)
                {
                    // Lấy DataRow tương ứng
                    DataRow selectedRow = dt.Rows[selectedIndex];

                    // Gán giá trị vào các TextBox
                    txtSellProductName.Text = selectedRow["ProductName"].ToString();
                    txtSellCategoryName.Text = selectedRow["CategoryName"].ToString();
                    txtSellSupplierName.Text = selectedRow["SupplierName"].ToString();
                    txtSellPrice.Text = selectedRow["ProductPrice"].ToString();
                    ptbSellImageProduct.Image = Image.FromFile(selectedRow["ProductImage"].ToString());
                }
            }
        }

        private void lbCheckSellID_TextChanged(object sender, EventArgs e)
        {
            lbCheckSellID.Visible = false;
        }
        private void lbCheckSellQuantity_TextChanged(object sender, EventArgs e)
        {
            lbCheckSellQuantity.Visible = false;
        }

        private void btnAddShoppingcart_Click(object sender, EventArgs e)
        {
            if (dgvShoppingCart.Columns.Count == 0)
            {
                dgvShoppingCart.Columns.Add("ProductID", "Mã sản phẩm");
                dgvShoppingCart.Columns.Add("ProductName", "Tên sản phẩm");
                dgvShoppingCart.Columns.Add("CategoryName", "Loại sản phẩm");
                dgvShoppingCart.Columns.Add("SupplierName", "Nhà cung cấp");
                dgvShoppingCart.Columns.Add("Quantity", "Số lượng");
                dgvShoppingCart.Columns.Add("PriceOut", "Giá");
                dgvShoppingCart.Columns.Add("TotalPrice", "Tổng tiền");
            }
            if (CheckAddShoppingCart())
            {
                // Lấy dòng được chọn từ ComboBox (là DataRowView)
                DataRowView selectedRow = (DataRowView)cboSellProductID.SelectedItem;
                // Truy cập giá trị ProductID từ DataRowView
                string productID = selectedRow["ProductID"].ToString();
                // Tạo đối tượng Product từ các TextBox
                Product product = new Product(
                    productID,
                    txtSellProductName.Text,
                    txtSellCategoryName.Text,
                    txtSellSupplierName.Text,
                    Convert.ToDecimal(txtSellPrice.Text),
                    int.Parse(txtSellQuantity.Text)
                );
                decimal totalprice = 0;
                // Kiểm tra nếu sản phẩm đã tồn tại trong DataGridView
                bool productExists = false;
                foreach (DataGridViewRow row in dgvShoppingCart.Rows)
                {
                    if (row.Cells["ProductID"].Value != null && row.Cells["ProductID"].Value.ToString() == product.ProductID)
                    {
                        // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                        int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                        int newQuantity = currentQuantity + product.ProductQuantity;
                        row.Cells["Quantity"].Value = newQuantity; // Cập nhật số lượng
                        row.Cells["TotalPrice"].Value = newQuantity * Convert.ToDecimal(row.Cells["PriceOut"].Value); // Cập nhật giá trị tổng
                        totalprice += newQuantity * Convert.ToDecimal(row.Cells["PriceOut"].Value);
                        productExists = true;
                        break;
                    }
                }

                // Nếu sản phẩm chưa tồn tại trong DataGridView, thêm sản phẩm mới
                if (!productExists)
                {
                    dgvShoppingCart.Rows.Add(product.ProductID, product.ProductName, product.CategoryName, product.SupplierName, product.ProductQuantity, product.ProductPrice, product.GetTotalPrice());
                }

                UpdatePrice();
            }

        }

        private void cboSellCustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox có dữ liệu hay không
            if (cboSellCustomerID.SelectedIndex != -1 && cboSellCustomerID.DataSource != null)
            {
                // Lấy DataTable từ DataSet
                DataTable dt = (DataTable)cboSellCustomerID.DataSource;

                // Lấy chỉ số của mục được chọn
                int selectedIndex = cboSellCustomerID.SelectedIndex;

                // Kiểm tra chỉ số hợp lệ
                if (selectedIndex >= 0 && selectedIndex < dt.Rows.Count)
                {
                    // Lấy DataRow tương ứng
                    DataRow selectedRow = dt.Rows[selectedIndex];

                    // Gán giá trị vào các TextBox
                    txtSellCutomerName.Text = selectedRow["CustomerName"].ToString();
                    txtSellAddressCustomer.Text = selectedRow["CustomerAddress"].ToString();
                    txtSellPhoneCustomer.Text = selectedRow["CustomerPhone"].ToString();
                    txtSellEmailCustomer.Text = selectedRow["CustomerEmail"].ToString();
                }
            }
        }

        private void dgvShoppingCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvShoppingCart.Rows.Count > 0 && e.RowIndex >= 0)
            {
                cboSellProductID.SelectedValue = dgvShoppingCart.Rows[e.RowIndex].Cells[0].Value;
                txtSellQuantity.Text = dgvShoppingCart.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtSellPrice.Text = dgvShoppingCart.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng hợp lệ.");
            }
        }

        private void btnUpdateShoppingcart_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox hoặc ComboBox
            // Lấy dòng được chọn từ ComboBox (là DataRowView)
            DataRowView selectedRow = (DataRowView)cboSellProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();
            int quantity = Convert.ToInt32(txtSellQuantity.Text); // Lấy số lượng từ TextBox
            decimal unitPrice = Convert.ToDecimal(txtSellPrice.Text); // Lấy giá từ TextBox

            // Tìm hàng trong DataGridView có ID sản phẩm trùng với productID
            foreach (DataGridViewRow row in dgvShoppingCart.Rows)
            {
                if (row.Cells["ProductID"].Value.ToString() == productID)
                {
                    // Cập nhật giá trị trong các ô tương ứng của dòng đó
                    row.Cells["Quantity"].Value = quantity;
                    row.Cells["PriceOut"].Value = unitPrice;
                    row.Cells["TotalPrice"].Value = unitPrice * quantity;
                    Console.WriteLine("GetToTalPrice:" + row.Cells["TotalPrice"].Value);
                    break; // Dừng vòng lặp sau khi tìm thấy dòng cần cập nhật
                }
            }

            // Cập nhật lại DataGridView (nếu cần thiết)
            dgvShoppingCart.Refresh();
        }

        private void btnDeleteShoppingcart_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ComboBox (DataRowView)
            DataRowView selectedRow = (DataRowView)cboSellProductID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string productID = selectedRow["ProductID"].ToString();

            // Duyệt qua các dòng trong DataGridView để tìm dòng có ProductID cần xóa
            foreach (DataGridViewRow row in dgvShoppingCart.Rows)
            {
                if (row.Cells["ProductID"].Value.ToString() == productID)
                {
                    // Xóa dòng tương ứng
                    dgvShoppingCart.Rows.RemoveAt(row.Index);
                    break; // Dừng vòng lặp sau khi xóa dòng cần tìm
                }
            }

            // Cập nhật lại DataGridView (nếu cần thiết)
            dgvShoppingCart.Refresh();
        }

        private void btnSaveShoppingCart_Click(object sender, EventArgs e)
        {
            dbSell dbSell = new dbSell();
            dbSellDetail dbSellDetail = new dbSellDetail();
            dbProduct dbProduct = new dbProduct();
            DataRowView selectedRow = (DataRowView)cboSellCustomerID.SelectedItem;

            // Truy cập giá trị ProductID từ DataRowView
            string customerID = selectedRow["CustomerID"].ToString();
            Product product = new Product();
            DataSet ds = dbProduct.GetProduct();
            decimal totalPrice = 0;
            foreach (DataGridViewRow row in dgvShoppingCart.Rows)
            {
                if (!row.IsNewRow) // Bỏ qua hàng trống (nếu có)
                {
                    var cellValue = row.Cells["TotalPrice"].Value;
                    // Chuyển đổi sang decimal
                    totalPrice += decimal.Parse(cellValue.ToString());
                }
            }
            // thêm hóa đơn bán sản phẩm 

            dbSell.InsertSell(txtSellID.Text, employee.Employeeid, customerID, decimal.Parse(lbTotalPrice.Text), datesell.Value);

            foreach (DataGridViewRow row in dgvShoppingCart.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow foundRow = null;
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        if (dataRow["ProductID"].ToString().Equals(row.Cells["ProductID"].Value.ToString()))
                        {
                            foundRow = dataRow;
                            break;
                        }
                    }
                    if (foundRow != null)
                    {
                        product.ProductID = foundRow["ProductID"].ToString();
                        product.ProductQuantity = int.Parse(foundRow["ProductQuantity"].ToString()) - int.Parse(row.Cells["Quantity"].Value.ToString());
                        product.ProductPrice = decimal.Parse(foundRow["ProductPrice"].ToString());
                        dbProduct.UpdateProductQuantity(row.Cells["ProductID"].Value.ToString(), product.ProductQuantity);
                        dbSellDetail.InsertSellDetail(txtSellID.Text, product.ProductID, int.Parse(row.Cells["Quantity"].Value.ToString()), product.ProductPrice);
                    }
                }
            }
            MessageBox.Show("Bán sản phẩm thành công!", "Thông báo");
            pnHoaDonBanHang.Visible = false;
            dgvShoppingCart.Rows.Clear();
            ResetHoaDonBan();
            LoadDataSell();
        }
        void UpdatePrice()
        {
            decimal TotalPrice = 0;
            decimal totalAmount = 0;
            int sell = 0;
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtSellProduct.Text)) // Nếu không nhập, mặc định là 0
            {
                sell = 0;
            }
            else if (!int.TryParse(txtSellProduct.Text, out sell))
            {
                lbCheckSellProduct.Text = "Vui lòng nhập đúng số";
                lbCheckSellProduct.ForeColor = Color.Red;
                lbCheckSellProduct.Visible = true;
                return; // Dừng xử lý nếu dữ liệu không hợp lệ
            }
            else if (sell < 0)
            {
                lbCheckSellProduct.Text = "Vui lòng nhập số dương";
                lbCheckSellProduct.ForeColor = Color.Red;
                lbCheckSellProduct.Visible = true;
                return;
            }
            else if (sell > 100)
            {
                lbCheckSellProduct.Text = "Vui lòng nhập số từ 0 đến 100";
                lbCheckSellProduct.ForeColor = Color.Red;
                lbCheckSellProduct.Visible = true;
                return;
            }
            else
            {
                lbCheckSellProduct.Visible = false; // Ẩn thông báo lỗi nếu nhập đúng
            }

            // Tính tổng tiền giỏ hàng
            foreach (DataGridViewRow row in dgvShoppingCart.Rows)
            {
                if (row.Cells["Quantity"].Value != null && row.Cells["PriceOut"].Value != null)
                {
                    int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal priceOut = Convert.ToDecimal(row.Cells["PriceOut"].Value);

                    // Cập nhật tổng tiền từng dòng
                    row.Cells["TotalPrice"].Value = currentQuantity * priceOut;

                    // Tích lũy tổng tiền
                    TotalPrice += currentQuantity * priceOut;
                }
            }

            // Nếu không nhập giảm giá hoặc nhập 0, tổng tiền là giá gốc
            if (sell == 0)
            {
                totalAmount = TotalPrice;
            }
            else
            {
                // Tính tổng sau khi áp dụng giảm giá
                decimal sellPercentage = (100 - sell) / 100m;
                totalAmount = TotalPrice * sellPercentage;
            }
            // Hiển thị tổng tiền (giá gốc hoặc sau giảm giá)
            lbTotalPrice.Text = totalAmount.ToString();
        }

        private void txtSellProduct_TextChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        private void txtFindSell_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFindSell.Text))
            {
                LoadDataSell();
                return;
            }

            dbSell dbSell = new dbSell();

            if (cboFindSell.SelectedIndex == 0)
            {
                // Tìm theo mã hóa đơn
                DataSet ds = dbSell.GetSellID(txtFindSell.Text);
                dgvDanhSachHoaDonBan.DataSource = null;
                dgvDanhSachHoaDonBan.DataSource = ds.Tables[0];
            }
            else if (cboFindSell.SelectedIndex == 1)
            {
                // Tìm theo ngày hóa đơn
                string input = txtFindSell.Text.Trim();

                // Không cần chuyển đổi sang DateTime, xử lý tìm kiếm chuỗi
                DataSet ds = dbSell.GetSellDate(input);
                dgvDanhSachHoaDonBan.DataSource = null;
                dgvDanhSachHoaDonBan.DataSource = ds.Tables[0];
            }
        }

        private void dgvDanhSachHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                pnChiTietHoaDonBanHang.Visible = true;
                pnChiTietHoaDonBanHang.BringToFront();
                txtMaHDBan.Text = dgvDanhSachHoaDonBan.Rows[e.RowIndex].Cells[0].Value.ToString();
                dbSellDetail dbSellDetail = new dbSellDetail();
                dgvChiTietHoaDonBan.DataSource = null;
                DataSet ds = dbSellDetail.GetSellDetail(txtMaHDBan.Text);
                dgvChiTietHoaDonBan.DataSource = ds.Tables[0];
            }

        }

        private void btnDeleteSell_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hóa đơn?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbSell dbSell = new dbSell();
                dbSell.DeleteSell(txtMaHDBan.Text);
                MessageBox.Show("Xóa thành công!", "Thông báo");
                pnChiTietHoaDonBanHang.Visible = false;
                LoadDataSell();
            }
        }

        private void ptbCloseSellDetail_Click(object sender, EventArgs e)
        {
            pnChiTietHoaDonBanHang.Visible = false;
            LoadDataSell();
        }

        private void btnCancelShoppingcart_Click(object sender, EventArgs e)
        {
            pnHoaDonBanHang.Visible = false;
        }
        #endregion


    }
}

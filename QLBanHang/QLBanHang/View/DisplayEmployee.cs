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

namespace QLBanHang.View
{
    public partial class DisplayEmployee : UserControl
    {
        #region Property
        string imageEmployee = "";
        Panel pnThemNhanVien;
        Employee employ;
        Guna2TextBox txtUser;
        Guna2TextBox txtPassword;
        Guna2TextBox txtEmployeeName;
        Guna2ComboBox cboGender;
        Guna2ComboBox cboRole;
        Guna2TextBox txtEmployeePhone;
        Guna2DateTimePicker cboBirthday;
        Guna2TextBox txtEmployeeAddress;
        Guna2TextBox txtEmployeeEmail;
        Guna2Button btnAddImageEmployee;
        Guna2Button btnUpdateEmployee;
        Guna2Button btnDeleteEmployee;
        Guna2HtmlLabel lbCheckUser;
        Guna2HtmlLabel lbCheckPass;
        Guna2HtmlLabel lbCheckName;
        Guna2HtmlLabel lbCheckEmail;
        Guna2HtmlLabel lbCheckPhone;
        Guna2HtmlLabel lbCheckAddress;
        #endregion
        public DisplayEmployee(Panel panel, Employee employee)
        {
            InitializeComponent();
            pnThemNhanVien = panel; // gán panel cha sang usercontrol để hiện thị
            employ = employee; 
            // Hiện thị một số thông tin nhân viên
            txtHoVaTen.Text = employee.EmployeeName;
            txtSoDienThoai.Text = employee.EmployeePhone;
            txtDiaChi.Text = employee.EmployeeAddress;
            ptbImageEmployee.Image = Image.FromFile(employee.Employeeimage);
            imageEmployee = employ.Employeeimage;
            Console.WriteLine("ID: " + employ.Employeeid);
        }
        // Hiện thị panel chứa đầy đủ thông tin của nhân viên khi click 2 lần vào Displayemployee.
        private void pnDisplayEmployee_DoubleClick(object sender, EventArgs e)
        {
            pnThemNhanVien.BringToFront();
            pnThemNhanVien.Visible = true;
            GetControl();
        }
        // Gán controls của panel cha sang panel kế thừa nó vì không thể lấy trực tiếp các textbox, label và combobox.
        public void GetControl()
        {
            txtUser = pnThemNhanVien.Controls["txtUser"] as Guna2TextBox; 
            txtUser.Text = employ.Employeeuser;
            txtPassword = pnThemNhanVien.Controls["txtPassword"] as Guna2TextBox;
            txtPassword.Text = employ.Employeepass;
            txtEmployeeName = pnThemNhanVien.Controls["txtEmployeeName"] as Guna2TextBox;
            txtEmployeeName.Text = employ.EmployeeName;
            cboGender = pnThemNhanVien.Controls["cboGender"] as Guna2ComboBox;
            cboGender.Text = employ.Gender;
            cboRole = pnThemNhanVien.Controls["cboChucVu"] as Guna2ComboBox;
            cboRole.Text = employ.Role;
            txtEmployeePhone = pnThemNhanVien.Controls["txtEmployeePhone"] as Guna2TextBox;
            txtEmployeePhone.Text = employ.EmployeePhone;
            cboBirthday = pnThemNhanVien.Controls["cboBirthday"] as Guna2DateTimePicker;
            cboBirthday.Value = employ.Birthday;
            txtEmployeeAddress = pnThemNhanVien.Controls["txtEmployeeAddress"] as Guna2TextBox;
            txtEmployeeAddress.Text = employ.EmployeeAddress;
            txtEmployeeEmail = pnThemNhanVien.Controls["txtEmployeeEmail"] as Guna2TextBox;
            txtEmployeeEmail.Text = employ.EmployeeEmail;
            ptbImageEmployee = pnThemNhanVien.Controls["ptbAnhDaiDien"] as Guna2PictureBox;
            ptbImageEmployee.Image = Image.FromFile(employ.Employeeimage);
            pnThemNhanVien.Controls["btnAddimageEmployee"].Visible = false;
            btnAddImageEmployee = pnThemNhanVien.Controls["btnChonAnh"] as Guna2Button;
            btnAddImageEmployee.Click += btnAddimageEmployee_Click;


            btnUpdateEmployee = pnThemNhanVien.Controls["btnUpdateEmployee"] as Guna2Button;
            btnUpdateEmployee.Visible = true;
            //btnUpdateEmployee.Click -= btnUpdateEmployee_Click;
            //btnUpdateEmployee.Click += btnUpdateEmployee_Click;
            btnUpdateEmployee.Click += btnUpdateEmployee_Click; // Gắn lại sự kiện mới



            btnDeleteEmployee = pnThemNhanVien.Controls["btnDeleteEmployee"] as Guna2Button;
            btnDeleteEmployee.Visible = true;
            btnDeleteEmployee.Click += btnDeleteEmployee_Click;
            pnThemNhanVien.Controls["btnEmployeeSave"].Visible = false;

            Guna2PictureBox ptbClose = pnThemNhanVien.Controls["ptbClose"] as Guna2PictureBox;
            ptbClose.Click += ptbClose_Click;

            // gán các label kiểm tra
            lbCheckUser = pnThemNhanVien.Controls["lbCheckUser"] as Guna2HtmlLabel;
            lbCheckPass = pnThemNhanVien.Controls["lbCheckPass"] as Guna2HtmlLabel;
            lbCheckPhone = pnThemNhanVien.Controls["lbCheckPhone"] as Guna2HtmlLabel;
            lbCheckAddress = pnThemNhanVien.Controls["lbCheckAddress"] as Guna2HtmlLabel;
            lbCheckEmail = pnThemNhanVien.Controls["lbCheckEmail"] as Guna2HtmlLabel;
            lbCheckName = pnThemNhanVien.Controls["lbCheckName"] as Guna2HtmlLabel;
        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            
            //pnThemNhanVien.Visible = false;
            btnUpdateEmployee.Click -= btnUpdateEmployee_Click; // Đảm bảo gỡ bỏ nếu đã được thêm
            btnDeleteEmployee.Click -= btnDeleteEmployee_Click;
            btnAddImageEmployee.Click -= btnAddimageEmployee_Click;
        }

        // Check thông tin trước khi update
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
                lbCheckAddress.Text = "Địa chỉ không được để trống";
                lbCheckAddress.ForeColor = Color.Red;
                lbCheckAddress.Visible = true;
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
        // Thay ảnh của nhân viên nếu update
        private void btnAddimageEmployee_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Thay ảnh";
            openFile.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Lọc file ảnh
            openFile.InitialDirectory = @"D:\images-NET"; // Thư mục mặc định khi mở
            openFile.Multiselect = false; // Chỉ cho phép chọn 1 ảnh
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imageEmployee = openFile.FileName; //Lấy đường dẫn của ảnh từ hộp thoại chọn file (openFile.FileName).
                ptbImageEmployee.Image = Image.FromFile(imageEmployee); // Tải ảnh từ đường dẫn đó vào PictureBox 
            }

        }
        #region Xóa nhân viên
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();
            if(MessageBox.Show("Bạn có muốn xóa nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbEmployee.DeleteEmployee(employ.Employeeid);
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
        }
        #endregion
        #region Cập nhật thông tin nhân viên
        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {

            Console.WriteLine("ID update:" + employ.Employeeid);
            DbEmployee dbEmployee = new DbEmployee();
            Console.WriteLine("Pass: " + employ.Employeepass);
            Console.WriteLine("Pass text: " + txtPassword.Text);
            if (txtPassword.Text.Equals(employ.Employeepass.Trim()))
            {
                txtPassword.Text = employ.Employeepass;

            }
            else
            {
                txtPassword.Text = Password.HashPassword(txtPassword.Text);
            }
            Console.WriteLine("Pass: " + txtPassword.Text);

            if (CheckEmployee())
            {
                if (dbEmployee.UpdateEmployee(txtUser.Text, txtPassword.Text,
                txtEmployeeName.Text, cboGender.SelectedItem.ToString(), cboRole.SelectedItem.ToString(), txtEmployeePhone.Text,
                cboBirthday.Value, txtEmployeeAddress.Text, txtEmployeeEmail.Text, imageEmployee, employ.Employeeid))
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thất bại!", "Thông báo");
                }
            }
        }
        #endregion
    }
}

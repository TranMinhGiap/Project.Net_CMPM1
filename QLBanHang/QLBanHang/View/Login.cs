using Guna.UI2.WinForms;
using QLBanHang.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBanHang.Controller;
using QLBanHang.Model;

namespace QLBanHang
{
    public partial class Login : Form
    {
        string otp;
        public Login()
        {
            InitializeComponent();
            // ẩn đổi pass nếu sai otp
            txtNewPassword.Visible = false;
            btnOK.Visible = false;
            lbSeperator.Visible = false;
            ptbSeeNewPass.Visible = false;
        }
        #region UX LOGIN
        private void btnLogin_Click(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> employees = dbEmployee.GetEmployee();
            foreach(Employee employee in employees)
            {
                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    lbCheckUser.Visible = true;
                    lbCheckUser.ForeColor = Color.Red;
                    lbCheckUser.Text = "Vui lòng nhập tài khoản";
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    lbCheckPassword.Visible = true;
                    lbCheckPassword.ForeColor = Color.Red;
                    lbCheckPassword.Text = "Vui lòng nhập mật khẩu";
                }
                else if (!(txtUser.Text.Equals(employee.Employeeuser) && employee.Employeepass.Equals(Password.HashPassword(txtPassword.Text))))
                {
                    lbCheckUser.Visible = true;
                    lbCheckUser.ForeColor = Color.Red;
                    lbCheckUser.Text = "Sai tài khoản hoặc mật khẩu";
                    lbCheckPassword.Visible = false;
                }
                else
                {
                    Function function = new Function(employee);
                    function.ShowDialog();
                    this.Close();
                }
            }
        }
        #endregion
        // đóng panel khôi phục mật khẩu
        private void ptbHide_Click(object sender, EventArgs e)
        {
            pnForgotPass.Visible = false;
        }
        // thoát chương trình
        private void ptbClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #region UI LOGIN

        private void lbForgotPass_Click(object sender, EventArgs e)
        {
            
            pnForgotPass.BringToFront();
            pnForgotPass.Visible = true;
        }

        private void txtQuenMatKhau_Click(object sender, EventArgs e)
        {
            pnForgotPass.BringToFront();
            pnForgotPass.Visible = true;
        }

        private void ptbSeePass_Click(object sender, EventArgs e)
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

        private void ptbSeeNewPass_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.PasswordChar.Equals('*'))
            {
                txtNewPassword.PasswordChar = '\0';
            }
            else
            {
                txtNewPassword.PasswordChar = '*';
            }
        }
        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            lbCheckUser.Visible = false;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lbCheckPassword.Visible = false;
        }
        #endregion
        #region Send OTP
        void SendEmail()
        {
            try
            {
                string fromEmail = "hieuhoangg2204@gmail.com"; // Email gửi OTP
                string appPassword = "lulggrjcumjjkcfp"; // Mật khẩu ứng dụng
                string toEmail = txtEmail.Text; // Email nhận OTP
                otp = GenerateOTP(); // Hàm tạo OTP

                // Cấu hình email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = "Your OTP Code";
                mail.Body = $"Your OTP code is: {otp}";

                // Cấu hình SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(fromEmail, appPassword),
                    EnableSsl = true
                };

                // Gửi email
                smtpClient.Send(mail);
                Console.WriteLine("OTP has been sent successfully!");
                MessageBox.Show("Vui lòng kiểm tra Email!", "Thông báo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 900000).ToString();
        }
        #endregion
        #region Xác thực OTP
        private void btnXacThuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOTP.Text))
            {
                lbCheckOTP.Visible = true;
                lbCheckOTP.ForeColor = Color.Red;
                lbCheckOTP.Text = "Vui lòng nhập OTP";

            }
            else if (!otp.Equals(txtOTP.Text))
            {
                lbCheckOTP.Visible = true;
                lbCheckOTP.ForeColor = Color.Red;
                lbCheckOTP.Text = "Sai OTP vui lòng nhập lại";
            }
            else
            {
                txtNewPassword.Visible = true;
                btnOK.Visible = true;
                lbSeperator.Visible = true;
                ptbSeeNewPass.Visible = true;
            }
        }
        #endregion
        #region UI OTP
        private void txtOTP_TextChanged(object sender, EventArgs e)
        {
            lbCheckOTP.Visible = false;
        }
        private void btnOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lbCheckEmail.Visible = true;
                lbCheckEmail.ForeColor = Color.Red;
                lbCheckEmail.Text = "Vui lòng nhập Email";
            }
            else
            {
                SendEmail();
            }
        }
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            lbCheckEmail.Visible = false;
        }
        #endregion
        #region UX Đổi mật khẩu
        private void btnOK_Click(object sender, EventArgs e)
        {
            DbEmployee dbEmployee = new DbEmployee();
            List<Employee> lst = dbEmployee.GetEmployee();
            foreach(Employee employee in lst)
            {
                if(employee.EmployeeEmail.Equals(txtEmail.Text))
                {
                    dbEmployee.SetPassword(txtEmail.Text, Password.HashPassword(txtNewPassword.Text));
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                    return;
                }
            }
        }

        #endregion

        
    }
}

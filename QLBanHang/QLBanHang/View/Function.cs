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
            btnTrangChu_Click(this, EventArgs.Empty);
            cboChart_SelectedIndexChanged(this, EventArgs.Empty);
        }
        
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
            if(MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            if(string.IsNullOrEmpty(txtSupplierID.Text))
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
            dgvSupplier.DataSource= ds.Tables[0];
            if (CheckSupplier())
            {
                if(ds.Tables[0].Rows.Count < 0)
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
            lbCheckSupplierEmail.Visible=false;
        }

        private void txtSupplierAddress_TextChanged(object sender, EventArgs e)
        {
            lbCheckSupplierAddress.Visible=false;
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
            if(e.RowIndex >= 0)
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
            if(txtSupplierID.Enabled == false)
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
            if(txtSupplierID.Enabled == false)
            {
                if(MessageBox.Show("Bạn có muốn xóa nhà cung cấp này không!", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            if(cboFindSupplier.SelectedIndex == 0)
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
            if(e.RowIndex >= 0)
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
            if(MessageBox.Show("Bạn có muốn xóa hóa đơn?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

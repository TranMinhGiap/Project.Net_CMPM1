using Guna.UI2.WinForms;
using QLBanHang.Controller;
using QLBanHang.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang.View
{
    public partial class DisplayProduct : UserControl
    {
        DataRow dataRow = null;
        Panel pnProduct;
        #region Property
        string imageProduct = "";
        Guna2TextBox txtProductID;
        Guna2TextBox txtProductName;
        Guna2TextBox txtProductQuantity;
        Guna2ComboBox cboCategoryID;
        Guna2ComboBox cboSupplierID;
        Guna2TextBox txtProductPrice;
        Guna2ComboBox cboProductStatus;
        Guna2TextBox txtProductDescription;
        Guna2Button btnSetImageProduct;
        Guna2Button btnProductUpdate;
        Guna2Button btnProductDelete;
        Guna2HtmlLabel lbCheckProductName;
        Guna2HtmlLabel lbCheckProductPrice;
        Guna2PictureBox ptbProductClose;
        Guna2PictureBox ptbImageProduct;
        #endregion
        public DisplayProduct(DataRow dr, Panel panel)
        {
            InitializeComponent();
            dataRow = dr;
            pnProduct = panel;
            LoadDataProduct();
        }
        void LoadDataProduct()
        {
            lbProductName.Text = dataRow["ProductName"].ToString();
            lbProductPrice.Text = dataRow["ProductPrice"].ToString();
            imageProduct = dataRow["ProductImage"].ToString();
            ptbAnhSanPham.Image = Image.FromFile(imageProduct);
        }

        private void pnDisplayProduct_DoubleClick(object sender, EventArgs e)
        {
            pnProduct.Visible = true;
            pnProduct.BringToFront();
            GetControl();
            txtProductID.Enabled = false;
        }
        bool CheckProduct()
        {
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
        void GetControl()
        {
            // gán controls của form cha sang user control
            txtProductID = pnProduct.Controls["txtProductID"] as Guna2TextBox;
            txtProductID.Text = dataRow["ProductID"].ToString();
            txtProductName = pnProduct.Controls["txtProductName"] as Guna2TextBox;
            txtProductName.Text = dataRow["ProductName"].ToString();
            txtProductQuantity = pnProduct.Controls["txtProductQuantity"] as Guna2TextBox;
            txtProductQuantity.Text = dataRow["ProductQuantity"].ToString();
            cboCategoryID = pnProduct.Controls["cboProductCategory"] as Guna2ComboBox;
            ptbImageProduct = pnProduct.Controls["ptbImageProduct"] as Guna2PictureBox;
            ptbImageProduct.Image = Image.FromFile(imageProduct);

            cboCategoryID.SelectedValue = dataRow["CategoryID"];
            Console.WriteLine("ID: " + cboCategoryID.SelectedValue);
            cboSupplierID = pnProduct.Controls["cboProductSupplier"] as Guna2ComboBox;
            cboSupplierID.SelectedValue = dataRow["SupplierID"];
            Console.WriteLine("ID: " + cboSupplierID.SelectedValue);

            txtProductPrice = pnProduct.Controls["txtProductPrice"] as Guna2TextBox;
            txtProductPrice.Text = dataRow["ProductPrice"].ToString();
            cboProductStatus = pnProduct.Controls["cboProductStatus"] as Guna2ComboBox;
            cboProductStatus.SelectedItem = dataRow["ProductStatus"].ToString();
            cboProductStatus.Enabled = true;
            txtProductDescription = pnProduct.Controls["txtProductDescription"] as Guna2TextBox;

            pnProduct.Controls["btnProductSave"].Visible = false;
            pnProduct.Controls["btnThemAnhSanPham"].Visible = false;
            btnProductUpdate = pnProduct.Controls["btnProductUpdate"] as Guna2Button;
            btnProductUpdate.Click += btnProductUpdate_Click;
            btnProductUpdate.Visible = true;
            btnProductDelete = pnProduct.Controls["btnProductDelete"] as Guna2Button;
            btnProductDelete.Click += btnProductDelete_Click;
            btnProductDelete.Visible = true;

            btnSetImageProduct = pnProduct.Controls["btnChonAnhSanPham"] as Guna2Button;
            btnSetImageProduct.Click += btnSetImageProduct_Click;
            btnSetImageProduct.Visible = true;
            ptbProductClose = pnProduct.Controls["ptbProductClose"] as Guna2PictureBox;
            ptbProductClose.Click += ptbProductClose_Click;


            lbCheckProductName = pnProduct.Controls["lbCheckProductName"] as Guna2HtmlLabel;
            lbCheckProductPrice = pnProduct.Controls["lbCheckProductPrice"] as Guna2HtmlLabel;
        }

        private void btnSetImageProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Thay ảnh";
            openFile.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Lọc file ảnh
            openFile.InitialDirectory = @"D:\images-NET"; // Thư mục mặc định khi mở
            openFile.Multiselect = false; // Chỉ cho phép chọn 1 ảnh
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                imageProduct = openFile.FileName; //Lấy đường dẫn của ảnh từ hộp thoại chọn file (openFile.FileName).
                ptbImageProduct.Image = Image.FromFile(imageProduct); // Tải ảnh từ đường dẫn đó vào PictureBox 
            }
            Console.WriteLine("Image: " + imageProduct);
        }


        private void ptbProductClose_Click(object sender, EventArgs e)
        {
            btnProductUpdate.Click -= btnProductUpdate_Click;
            btnProductDelete.Click -= btnProductDelete_Click;
            btnSetImageProduct.Click -= btnSetImageProduct_Click;
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            dbProduct dbProduct = new dbProduct();
            if(MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dbProduct.ProductDelete(txtProductID.Text);
                MessageBox.Show("Xóa thành công", "Thông báo");
            }
        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            if (CheckProduct())
            {
                dbProduct dbProduct = new dbProduct();
                dbProduct.UpdateProduct(txtProductID.Text, txtProductName.Text, int.Parse(txtProductQuantity.Text), 
                    cboCategoryID.SelectedValue.ToString(), cboSupplierID.SelectedValue.ToString(), 
                    decimal.Parse(txtProductPrice.Text), cboProductStatus.SelectedItem.ToString(), 
                    txtProductDescription.Text, imageProduct);
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
            }
        }
    }
}

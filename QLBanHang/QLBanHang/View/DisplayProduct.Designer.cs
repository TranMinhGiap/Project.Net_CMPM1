namespace QLBanHang.View
{
    partial class DisplayProduct
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnDisplayProduct = new Guna.UI2.WinForms.Guna2Panel();
            this.lbProductPrice = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbProductName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ptbAnhSanPham = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnDisplayProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAnhSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // pnDisplayProduct
            // 
            this.pnDisplayProduct.BorderColor = System.Drawing.Color.Black;
            this.pnDisplayProduct.BorderRadius = 30;
            this.pnDisplayProduct.BorderThickness = 1;
            this.pnDisplayProduct.Controls.Add(this.lbProductPrice);
            this.pnDisplayProduct.Controls.Add(this.lbProductName);
            this.pnDisplayProduct.Controls.Add(this.guna2HtmlLabel2);
            this.pnDisplayProduct.Controls.Add(this.guna2HtmlLabel1);
            this.pnDisplayProduct.Controls.Add(this.ptbAnhSanPham);
            this.pnDisplayProduct.FillColor = System.Drawing.Color.White;
            this.pnDisplayProduct.Location = new System.Drawing.Point(0, 0);
            this.pnDisplayProduct.Name = "pnDisplayProduct";
            this.pnDisplayProduct.Size = new System.Drawing.Size(382, 384);
            this.pnDisplayProduct.TabIndex = 0;
            this.pnDisplayProduct.DoubleClick += new System.EventHandler(this.pnDisplayProduct_DoubleClick);
            // 
            // lbProductPrice
            // 
            this.lbProductPrice.BackColor = System.Drawing.Color.Transparent;
            this.lbProductPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductPrice.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lbProductPrice.Location = new System.Drawing.Point(131, 343);
            this.lbProductPrice.Name = "lbProductPrice";
            this.lbProductPrice.Size = new System.Drawing.Size(3, 2);
            this.lbProductPrice.TabIndex = 6;
            // 
            // lbProductName
            // 
            this.lbProductName.BackColor = System.Drawing.Color.Transparent;
            this.lbProductName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductName.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lbProductName.Location = new System.Drawing.Point(131, 284);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(3, 2);
            this.lbProductName.TabIndex = 5;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(3, 343);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(36, 27);
            this.guna2HtmlLabel2.TabIndex = 4;
            this.guna2HtmlLabel2.Text = "Giá:";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(3, 284);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(65, 27);
            this.guna2HtmlLabel1.TabIndex = 3;
            this.guna2HtmlLabel1.Text = "Tên SP:";
            // 
            // ptbAnhSanPham
            // 
            this.ptbAnhSanPham.AutoRoundedCorners = true;
            this.ptbAnhSanPham.BorderRadius = 126;
            this.ptbAnhSanPham.ImageRotate = 0F;
            this.ptbAnhSanPham.Location = new System.Drawing.Point(44, 23);
            this.ptbAnhSanPham.Name = "ptbAnhSanPham";
            this.ptbAnhSanPham.Size = new System.Drawing.Size(298, 255);
            this.ptbAnhSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbAnhSanPham.TabIndex = 0;
            this.ptbAnhSanPham.TabStop = false;
            // 
            // DisplayProduct
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnDisplayProduct);
            this.Name = "DisplayProduct";
            this.Size = new System.Drawing.Size(382, 384);
            this.pnDisplayProduct.ResumeLayout(false);
            this.pnDisplayProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAnhSanPham)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnDisplayProduct;
        private Guna.UI2.WinForms.Guna2PictureBox ptbAnhSanPham;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbProductPrice;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbProductName;
    }
}

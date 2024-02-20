namespace TravelPackageGUI
{
    partial class frmAddModifyPackage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtName = new TextBox();
            txtStartDate = new TextBox();
            txtEndDate = new TextBox();
            txtDesc = new TextBox();
            txtPrice = new TextBox();
            txtCommission = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            txtPackageID = new TextBox();
            pnlHider = new Panel();
            dgvProducts = new DataGridView();
            ProdSupID = new DataGridViewTextBoxColumn();
            ProdID = new DataGridViewTextBoxColumn();
            ProdName = new DataGridViewTextBoxColumn();
            SupplierId = new DataGridViewTextBoxColumn();
            SupplierName = new DataGridViewTextBoxColumn();
            ccbCheck = new DataGridViewCheckBoxColumn();
            lstProducts = new ListBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 92);
            label2.Name = "label2";
            label2.Size = new Size(132, 25);
            label2.TabIndex = 1;
            label2.Text = "Package Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 138);
            label3.Name = "label3";
            label3.Size = new Size(94, 25);
            label3.TabIndex = 2;
            label3.Text = "Start Date:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 185);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 3;
            label4.Text = "End Date:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(45, 231);
            label5.Name = "label5";
            label5.Size = new Size(106, 25);
            label5.TabIndex = 4;
            label5.Text = "Description:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(45, 345);
            label6.Name = "label6";
            label6.Size = new Size(94, 25);
            label6.TabIndex = 5;
            label6.Text = "Base Price:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(45, 391);
            label7.Name = "label7";
            label7.Size = new Size(179, 25);
            label7.TabIndex = 6;
            label7.Text = "Agency Commission:";
            // 
            // txtName
            // 
            txtName.Location = new Point(230, 89);
            txtName.Name = "txtName";
            txtName.Size = new Size(241, 31);
            txtName.TabIndex = 8;
            txtName.Tag = "Package Name";
            // 
            // txtStartDate
            // 
            txtStartDate.Location = new Point(230, 135);
            txtStartDate.Name = "txtStartDate";
            txtStartDate.PlaceholderText = "YYYY-MM-DD";
            txtStartDate.Size = new Size(241, 31);
            txtStartDate.TabIndex = 9;
            txtStartDate.Tag = "Start Date";
            // 
            // txtEndDate
            // 
            txtEndDate.Location = new Point(230, 182);
            txtEndDate.Name = "txtEndDate";
            txtEndDate.PlaceholderText = "YYYY-MM-DD";
            txtEndDate.Size = new Size(241, 31);
            txtEndDate.TabIndex = 10;
            txtEndDate.Tag = "End Date";
            // 
            // txtDesc
            // 
            txtDesc.Location = new Point(230, 228);
            txtDesc.MaxLength = 50;
            txtDesc.Multiline = true;
            txtDesc.Name = "txtDesc";
            txtDesc.ScrollBars = ScrollBars.Vertical;
            txtDesc.Size = new Size(241, 98);
            txtDesc.TabIndex = 11;
            txtDesc.Tag = "Description";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(230, 342);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(241, 31);
            txtPrice.TabIndex = 12;
            txtPrice.Tag = "Base Price";
            // 
            // txtCommission
            // 
            txtCommission.Location = new Point(230, 388);
            txtCommission.Name = "txtCommission";
            txtCommission.Size = new Size(241, 31);
            txtCommission.TabIndex = 13;
            txtCommission.Tag = "Agency Commission";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(137, 607);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 14;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(255, 607);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 48);
            label1.Name = "label1";
            label1.Size = new Size(103, 25);
            label1.TabIndex = 16;
            label1.Text = "Package ID:";
            // 
            // txtPackageID
            // 
            txtPackageID.Location = new Point(230, 42);
            txtPackageID.Name = "txtPackageID";
            txtPackageID.ReadOnly = true;
            txtPackageID.Size = new Size(241, 31);
            txtPackageID.TabIndex = 17;
            txtPackageID.Tag = "Package Name";
            // 
            // pnlHider
            // 
            pnlHider.Location = new Point(45, 42);
            pnlHider.Name = "pnlHider";
            pnlHider.Size = new Size(426, 38);
            pnlHider.TabIndex = 18;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.BackgroundColor = SystemColors.Control;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { ProdSupID, ProdID, ProdName, SupplierId, SupplierName, ccbCheck });
            dgvProducts.Location = new Point(519, 42);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.Size = new Size(1011, 548);
            dgvProducts.TabIndex = 19;
            // 
            // ProdSupID
            // 
            ProdSupID.HeaderText = "PSID";
            ProdSupID.MinimumWidth = 8;
            ProdSupID.Name = "ProdSupID";
            ProdSupID.ReadOnly = true;
            ProdSupID.Width = 70;
            // 
            // ProdID
            // 
            ProdID.HeaderText = "Product ID";
            ProdID.MinimumWidth = 8;
            ProdID.Name = "ProdID";
            ProdID.ReadOnly = true;
            ProdID.Width = 150;
            // 
            // ProdName
            // 
            ProdName.HeaderText = "Product Name";
            ProdName.MinimumWidth = 8;
            ProdName.Name = "ProdName";
            ProdName.ReadOnly = true;
            ProdName.Width = 250;
            // 
            // SupplierId
            // 
            SupplierId.HeaderText = "Supplier ID";
            SupplierId.MinimumWidth = 8;
            SupplierId.Name = "SupplierId";
            SupplierId.ReadOnly = true;
            SupplierId.Width = 150;
            // 
            // SupplierName
            // 
            SupplierName.HeaderText = "Supplier Name";
            SupplierName.MinimumWidth = 8;
            SupplierName.Name = "SupplierName";
            SupplierName.ReadOnly = true;
            SupplierName.Width = 250;
            // 
            // ccbCheck
            // 
            ccbCheck.HeaderText = "";
            ccbCheck.MinimumWidth = 8;
            ccbCheck.Name = "ccbCheck";
            ccbCheck.Width = 50;
            // 
            // lstProducts
            // 
            lstProducts.FormattingEnabled = true;
            lstProducts.ItemHeight = 25;
            lstProducts.Location = new Point(137, 439);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new Size(334, 129);
            lstProducts.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(45, 439);
            label8.Name = "label8";
            label8.Size = new Size(86, 25);
            label8.TabIndex = 21;
            label8.Text = "Products:";
            // 
            // frmAddModifyPackage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1567, 672);
            Controls.Add(label8);
            Controls.Add(lstProducts);
            Controls.Add(dgvProducts);
            Controls.Add(pnlHider);
            Controls.Add(txtPackageID);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtCommission);
            Controls.Add(txtPrice);
            Controls.Add(txtDesc);
            Controls.Add(txtEndDate);
            Controls.Add(txtStartDate);
            Controls.Add(txtName);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "frmAddModifyPackage";
            Text = "frmAddModifyPackage";
            Load += frmAddModifyPackage_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtName;
        private TextBox txtStartDate;
        private TextBox txtEndDate;
        private TextBox txtDesc;
        private TextBox txtPrice;
        private TextBox txtCommission;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private TextBox txtPackageID;
        private Panel pnlHider;
        private DataGridView dgvProducts;
        private ListBox lstProducts;
        private Label label8;
        private DataGridViewTextBoxColumn ProdSupID;
        private DataGridViewTextBoxColumn ProdID;
        private DataGridViewTextBoxColumn ProdName;
        private DataGridViewTextBoxColumn SupplierId;
        private DataGridViewTextBoxColumn SupplierName;
        private DataGridViewCheckBoxColumn ccbCheck;
    }
}
namespace ThreadedWorkshop4
{
    partial class TravelPackageManager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            cboPackageName = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtID = new TextBox();
            txtStartDate = new TextBox();
            txtEndDate = new TextBox();
            txtDesc = new TextBox();
            txtBasePrice = new TextBox();
            txtCommission = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            button1 = new Button();
            dgvProducts = new DataGridView();
            ProdSupID = new DataGridViewTextBoxColumn();
            ProdID = new DataGridViewTextBoxColumn();
            ProdName = new DataGridViewTextBoxColumn();
            SupplierId = new DataGridViewTextBoxColumn();
            SupplierName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // cboPackageName
            // 
            cboPackageName.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPackageName.FormattingEnabled = true;
            cboPackageName.Location = new Point(253, 44);
            cboPackageName.Name = "cboPackageName";
            cboPackageName.Size = new Size(252, 33);
            cboPackageName.TabIndex = 0;
            cboPackageName.SelectedIndexChanged += cboProductID_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 44);
            label1.Name = "label1";
            label1.Size = new Size(132, 25);
            label1.TabIndex = 1;
            label1.Text = "Package Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 101);
            label2.Name = "label2";
            label2.Size = new Size(103, 25);
            label2.TabIndex = 2;
            label2.Text = "Package ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 141);
            label3.Name = "label3";
            label3.Size = new Size(94, 25);
            label3.TabIndex = 3;
            label3.Text = "Start Date:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 182);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 4;
            label4.Text = "End Date:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(36, 222);
            label5.Name = "label5";
            label5.Size = new Size(106, 25);
            label5.TabIndex = 5;
            label5.Text = "Description:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(36, 349);
            label6.Name = "label6";
            label6.Size = new Size(94, 25);
            label6.TabIndex = 6;
            label6.Text = "Base Price:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(36, 390);
            label7.Name = "label7";
            label7.Size = new Size(179, 25);
            label7.TabIndex = 7;
            label7.Text = "Agency Commission:";
            // 
            // txtID
            // 
            txtID.Location = new Point(253, 98);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(252, 31);
            txtID.TabIndex = 8;
            // 
            // txtStartDate
            // 
            txtStartDate.Location = new Point(253, 138);
            txtStartDate.Name = "txtStartDate";
            txtStartDate.ReadOnly = true;
            txtStartDate.Size = new Size(252, 31);
            txtStartDate.TabIndex = 9;
            // 
            // txtEndDate
            // 
            txtEndDate.Location = new Point(253, 179);
            txtEndDate.Name = "txtEndDate";
            txtEndDate.ReadOnly = true;
            txtEndDate.Size = new Size(252, 31);
            txtEndDate.TabIndex = 10;
            // 
            // txtDesc
            // 
            txtDesc.Location = new Point(253, 219);
            txtDesc.MaxLength = 500;
            txtDesc.Multiline = true;
            txtDesc.Name = "txtDesc";
            txtDesc.ReadOnly = true;
            txtDesc.ScrollBars = ScrollBars.Vertical;
            txtDesc.Size = new Size(252, 121);
            txtDesc.TabIndex = 11;
            // 
            // txtBasePrice
            // 
            txtBasePrice.Location = new Point(253, 346);
            txtBasePrice.Name = "txtBasePrice";
            txtBasePrice.ReadOnly = true;
            txtBasePrice.Size = new Size(252, 31);
            txtBasePrice.TabIndex = 12;
            // 
            // txtCommission
            // 
            txtCommission.Location = new Point(253, 387);
            txtCommission.Name = "txtCommission";
            txtCommission.ReadOnly = true;
            txtCommission.Size = new Size(252, 31);
            txtCommission.TabIndex = 13;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(36, 448);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 34);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "&Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(215, 448);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(112, 34);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "&Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(393, 448);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(112, 34);
            btnUpdate.TabIndex = 16;
            btnUpdate.Text = "&Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // button1
            // 
            button1.Location = new Point(1379, 448);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 17;
            button1.Text = "&Exit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { ProdSupID, ProdID, ProdName, SupplierId, SupplierName });
            dgvProducts.Location = new Point(548, 44);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 62;
            dgvProducts.Size = new Size(943, 374);
            dgvProducts.TabIndex = 18;
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
            // TravelPackageManager
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1548, 516);
            Controls.Add(dgvProducts);
            Controls.Add(button1);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(txtCommission);
            Controls.Add(txtBasePrice);
            Controls.Add(txtDesc);
            Controls.Add(txtEndDate);
            Controls.Add(txtStartDate);
            Controls.Add(txtID);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cboPackageName);
            Name = "TravelPackageManager";
            Text = "Package Manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cboPackageName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtID;
        private TextBox txtStartDate;
        private TextBox txtEndDate;
        private TextBox txtDesc;
        private TextBox txtBasePrice;
        private TextBox txtCommission;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private Button button1;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn ProdSupID;
        private DataGridViewTextBoxColumn ProdID;
        private DataGridViewTextBoxColumn ProdName;
        private DataGridViewTextBoxColumn SupplierId;
        private DataGridViewTextBoxColumn SupplierName;
    }
}

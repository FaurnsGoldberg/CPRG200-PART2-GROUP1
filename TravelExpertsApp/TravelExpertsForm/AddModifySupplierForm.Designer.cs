namespace TravelExpertsForm;

partial class AddModifySupplierForm
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
        label2 = new Label();
        txtName = new TextBox();
        lblSupId = new Label();
        txtSupplierId = new TextBox();
        btnCancel = new Button();
        btnAccept = new Button();
        label3 = new Label();
        cmbContacts = new ComboBox();
        grbContact = new GroupBox();
        btnContactSave = new Button();
        label5 = new Label();
        txtEmail = new TextBox();
        label6 = new Label();
        txtCompany = new TextBox();
        label4 = new Label();
        txtLastName = new TextBox();
        label1 = new Label();
        txtFirstName = new TextBox();
        btnAddContact = new Button();
        btnDeleteContact = new Button();
        grbContact.SuspendLayout();
        SuspendLayout();
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(18, 257);
        label2.Margin = new Padding(2, 0, 2, 0);
        label2.Name = "label2";
        label2.Size = new Size(88, 15);
        label2.TabIndex = 12;
        label2.Text = "Supplier Name:";
        // 
        // txtName
        // 
        txtName.Location = new Point(110, 255);
        txtName.Margin = new Padding(2);
        txtName.Name = "txtName";
        txtName.Size = new Size(220, 23);
        txtName.TabIndex = 13;
        // 
        // lblSupId
        // 
        lblSupId.AutoSize = true;
        lblSupId.Location = new Point(18, 227);
        lblSupId.Margin = new Padding(2, 0, 2, 0);
        lblSupId.Name = "lblSupId";
        lblSupId.Size = new Size(67, 15);
        lblSupId.TabIndex = 9;
        lblSupId.Text = "Supplier ID:";
        // 
        // txtSupplierId
        // 
        txtSupplierId.Location = new Point(110, 225);
        txtSupplierId.Margin = new Padding(2);
        txtSupplierId.Name = "txtSupplierId";
        txtSupplierId.ReadOnly = true;
        txtSupplierId.Size = new Size(99, 23);
        txtSupplierId.TabIndex = 10;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(119, 287);
        btnCancel.Margin = new Padding(2);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(90, 24);
        btnCancel.TabIndex = 19;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnAccept
        // 
        btnAccept.Location = new Point(18, 287);
        btnAccept.Margin = new Padding(2);
        btnAccept.Name = "btnAccept";
        btnAccept.Size = new Size(90, 24);
        btnAccept.TabIndex = 18;
        btnAccept.Text = "Accept";
        btnAccept.UseVisualStyleBackColor = true;
        btnAccept.Click += btnAccept_Click;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(11, 19);
        label3.Name = "label3";
        label3.Size = new Size(54, 15);
        label3.TabIndex = 21;
        label3.Text = "Contacts";
        // 
        // cmbContacts
        // 
        cmbContacts.FormattingEnabled = true;
        cmbContacts.Location = new Point(12, 37);
        cmbContacts.Name = "cmbContacts";
        cmbContacts.Size = new Size(399, 23);
        cmbContacts.TabIndex = 22;
        cmbContacts.SelectedIndexChanged += cmbContacts_SelectedIndexChanged;
        // 
        // grbContact
        // 
        grbContact.Controls.Add(btnContactSave);
        grbContact.Controls.Add(label5);
        grbContact.Controls.Add(txtEmail);
        grbContact.Controls.Add(label6);
        grbContact.Controls.Add(txtCompany);
        grbContact.Controls.Add(label4);
        grbContact.Controls.Add(txtLastName);
        grbContact.Controls.Add(label1);
        grbContact.Controls.Add(txtFirstName);
        grbContact.Location = new Point(12, 76);
        grbContact.Name = "grbContact";
        grbContact.Size = new Size(752, 134);
        grbContact.TabIndex = 23;
        grbContact.TabStop = false;
        grbContact.Visible = false;
        // 
        // btnContactSave
        // 
        btnContactSave.Location = new Point(6, 89);
        btnContactSave.Margin = new Padding(2);
        btnContactSave.Name = "btnContactSave";
        btnContactSave.Size = new Size(101, 24);
        btnContactSave.TabIndex = 26;
        btnContactSave.Text = "Save Contact";
        btnContactSave.UseVisualStyleBackColor = true;
        btnContactSave.Click += btnContactSave_Click;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(408, 21);
        label5.Margin = new Padding(2, 0, 2, 0);
        label5.Name = "label5";
        label5.Size = new Size(39, 15);
        label5.TabIndex = 31;
        label5.Text = "Email:";
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(450, 13);
        txtEmail.Margin = new Padding(2);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(160, 23);
        txtEmail.TabIndex = 32;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(6, 59);
        label6.Margin = new Padding(2, 0, 2, 0);
        label6.Name = "label6";
        label6.Size = new Size(62, 15);
        label6.TabIndex = 29;
        label6.Text = "Company:";
        // 
        // txtCompany
        // 
        txtCompany.Location = new Point(77, 51);
        txtCompany.Margin = new Padding(2);
        txtCompany.Name = "txtCompany";
        txtCompany.Size = new Size(370, 23);
        txtCompany.TabIndex = 30;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(197, 21);
        label4.Margin = new Padding(2, 0, 2, 0);
        label4.Name = "label4";
        label4.Size = new Size(66, 15);
        label4.TabIndex = 27;
        label4.Text = "Last Name:";
        // 
        // txtLastName
        // 
        txtLastName.Location = new Point(266, 13);
        txtLastName.Margin = new Padding(2);
        txtLastName.Name = "txtLastName";
        txtLastName.Size = new Size(133, 23);
        txtLastName.TabIndex = 28;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 21);
        label1.Margin = new Padding(2, 0, 2, 0);
        label1.Name = "label1";
        label1.Size = new Size(67, 15);
        label1.TabIndex = 25;
        label1.Text = "First Name:";
        // 
        // txtFirstName
        // 
        txtFirstName.Location = new Point(77, 13);
        txtFirstName.Margin = new Padding(2);
        txtFirstName.Name = "txtFirstName";
        txtFirstName.Size = new Size(113, 23);
        txtFirstName.TabIndex = 26;
        // 
        // btnAddContact
        // 
        btnAddContact.Location = new Point(425, 19);
        btnAddContact.Margin = new Padding(2);
        btnAddContact.Name = "btnAddContact";
        btnAddContact.Size = new Size(101, 24);
        btnAddContact.TabIndex = 24;
        btnAddContact.Text = "New Contact";
        btnAddContact.UseVisualStyleBackColor = true;
        btnAddContact.Click += btnAddContact_Click;
        // 
        // btnDeleteContact
        // 
        btnDeleteContact.Location = new Point(530, 19);
        btnDeleteContact.Margin = new Padding(2);
        btnDeleteContact.Name = "btnDeleteContact";
        btnDeleteContact.Size = new Size(101, 24);
        btnDeleteContact.TabIndex = 25;
        btnDeleteContact.Text = "Delete Contact";
        btnDeleteContact.UseVisualStyleBackColor = true;
        btnDeleteContact.Click += btnDeleteContact_Click;
        // 
        // AddModifySupplierForm
        // 
        AcceptButton = btnAccept;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(799, 368);
        ControlBox = false;
        Controls.Add(btnDeleteContact);
        Controls.Add(btnAddContact);
        Controls.Add(grbContact);
        Controls.Add(cmbContacts);
        Controls.Add(label3);
        Controls.Add(btnCancel);
        Controls.Add(btnAccept);
        Controls.Add(label2);
        Controls.Add(txtName);
        Controls.Add(lblSupId);
        Controls.Add(txtSupplierId);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(2);
        MaximizeBox = false;
        Name = "AddModifySupplierForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Add a supplier";
        Load += frmAddModify_Load;
        grbContact.ResumeLayout(false);
        grbContact.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Label label2;
    private TextBox txtName;
    private Label lblSupId;
    private TextBox txtSupplierId;
    private Button btnCancel;
    private Button btnAccept;
    private Label label3;
    private ComboBox cmbContacts;
    private GroupBox grbContact;
    private Label label4;
    private TextBox txtLastName;
    private Label label1;
    private TextBox txtFirstName;
    private Label label5;
    private TextBox txtEmail;
    private Label label6;
    private TextBox txtCompany;
    private Button btnAddContact;
    private Button btnDeleteContact;
    private Button btnContactSave;
}
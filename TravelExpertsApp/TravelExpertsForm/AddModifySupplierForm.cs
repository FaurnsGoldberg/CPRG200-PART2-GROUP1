using TravelExpertsSuppliersDB.Models;
using TravelExpertsSuppliersDB;

namespace TravelExpertsForm;
/// <summary>
/// Form used when adding or modifying an existing supplier
/// </summary>
/// <author>Michael Chessall</author>
public partial class AddModifySupplierForm : Form
{
    private SupplierContact selectedSupplierContact = null!;
    public AddModifySupplierForm()
    {
        InitializeComponent();
    }

    public Supplier supplier { get; set; } = null!;

    /// <summary>
    /// Adds each contact to the dropdown list
    /// </summary>
    private void DisplayContacts()  
    {
        cmbContacts.Items.Clear();
        List<SupplierContact> contacts = TravelExpertsDataAccess.GetSupplierContacts(supplier);
        foreach (var item in contacts)
        {
            cmbContacts.Items.Add(item);
        }
    }
    /// <summary>
    /// Create a new contact and add it to the current supplier.
    /// </summary>
    private void AddContact()
    {
        SupplierContact contact = new SupplierContact();
        contact.SupplierContactId = TravelExpertsDataAccess.GetContactId();
        contact.SupConCompany = "New Contact";
        contact.SupplierId = supplier.SupplierId;
        supplier.SupplierContacts.Add(contact);
        TravelExpertsDataAccess.AddSupplierContact(contact);
        TravelExpertsDataAccess.UpdateSupplier(supplier);
        cmbContacts.Items.Add(contact);
        cmbContacts.SelectedItem = contact;

    }
    /// <summary>
    /// Hides the group box which displays contact data
    /// </summary>
    private void HideContactData()
    {
        grbContact.Visible = false;
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtCompany.Text = "";
        txtEmail.Text = "";
    }
    /// <summary>
    /// displays the group box which displays contact data and updates textboxes to match the suppliercontact
    /// </summary>
    /// <param name="suppliercontact">the selected suppliercontact to pull data from</param>
    private void ShowContactData(SupplierContact suppliercontact)
    {
        grbContact.Visible = true;
        txtFirstName.Text = suppliercontact.SupConFirstName;
        txtLastName.Text = suppliercontact.SupConLastName;
        txtCompany.Text = suppliercontact.SupConCompany;
        txtEmail.Text = suppliercontact.SupConEmail;

    }

    /// <summary>
    /// when the contacts dropdown list selection is changed
    /// </summary>
    private void cmbContacts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbContacts.SelectedItem != null)
        {
            SupplierContact contact = (SupplierContact)cmbContacts.SelectedItem;
            ShowContactData(contact);
        }
        else // selected contact is null, hide contact data
        {
            HideContactData();
        }
    }

    private void frmAddModify_Load(object sender, EventArgs e)
    {
        if (supplier == null)
        {
            AddSupplierSetup();
        }
        else
        {

            ModifySupplierSetup();
        }
    }
    /// <summary>
    /// setup the form for modifying an existing supplier
    /// </summary>
    private void ModifySupplierSetup()
    {
        Text = "Modify Supplier";
        txtSupplierId.Text = supplier.SupplierId.ToString();
        txtName.Text = supplier.SupName;
        DisplayContacts();
        txtName.Focus();
    }
    /// <summary>
    /// setup the form for adding a new supplier
    /// </summary>
    private void AddSupplierSetup()
    {
        supplier = new Supplier();
        supplier.SupplierId = TravelExpertsDataAccess.GetSupplierId();
        txtSupplierId.Text = supplier.SupplierId.ToString();
        supplier.SupName = "New Supplier";
        TravelExpertsDataAccess.AddSupplier(supplier);
        Text = "Add Supplier";
        DisplayContacts();
        txtName.Focus();
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
        if (IsValidData())
        {
            LoadSupplierData();
            DialogResult = DialogResult.OK;
        }
    }

    private bool IsValidData()
    {
        bool success = true;
        string errorMessage = "";

        errorMessage += Validator.IsPresent(txtName.Text, "Name");
        errorMessage += Validator.IsWithinLength(txtName.Text, "Name", 1, 50); //  varchar(50)

        if (!string.IsNullOrEmpty(errorMessage))
        {
            success = false;
            MessageBox.Show(errorMessage, "Entry Error");
        }
        return success;
    }

    private void LoadSupplierData()
    {
        supplier.SupName = txtName.Text;

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void btnAddContact_Click(object sender, EventArgs e)
    {
        AddContact();
    }

    /// <summary>
    /// deletes the selected contact
    /// </summary>
    private void btnDeleteContact_Click(object sender, EventArgs e)
    {
        if (cmbContacts.SelectedItem != null)
        {
            TravelExpertsDataAccess.RemoveSupplierContact(supplier, (SupplierContact)cmbContacts.SelectedItem);
            cmbContacts.Items.Remove(cmbContacts.SelectedItem);
            cmbContacts.SelectedItem = null;
            HideContactData();
        }

    }
    /// <summary>
    /// saves changes made to the current contact
    /// </summary>
    private void btnContactSave_Click(object sender, EventArgs e)
    {
        if (IsValidContactData() && cmbContacts.SelectedItem != null)
        {
            SupplierContact contact = (SupplierContact)cmbContacts.SelectedItem;
            contact.SupConCompany = txtCompany.Text;
            contact.SupConFirstName = txtFirstName.Text;
            contact.SupConLastName = txtLastName.Text;
            contact.SupConEmail = txtEmail.Text;
            TravelExpertsDataAccess.UpdateSupplierContact(contact);
            DisplayContacts();
            cmbContacts.SelectedItem = contact;
        }
    }

    private bool IsValidContactData()
    {
        bool success = true;
        string errorMessage = "";

        errorMessage += Validator.IsWithinLength(txtFirstName.Text, "First Name", 0, 50); //  nvarchar(50)
        errorMessage += Validator.IsWithinLength(txtLastName.Text, "Last Name", 0, 50); //  nvarchar(50)
        errorMessage += Validator.IsWithinLength(txtCompany.Text, "Company", 0, 255); //  nvarchar(255)
        errorMessage += Validator.IsWithinLength(txtEmail.Text, "Email", 0, 255); //  nvarchar(255)

        if (!string.IsNullOrEmpty(errorMessage))
        {
            success = false;
            MessageBox.Show(errorMessage, "Entry Error");
        }
        return success;
    }
}

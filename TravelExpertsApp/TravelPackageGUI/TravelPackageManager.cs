using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security;
using System.Windows.Forms;
using TravelPackageData;
using TravelPackageData.Models;
using TravelPackageGUI;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThreadedWorkshop4
{
    public partial class TravelPackageManager : Form
    {
        private Package? selectedPackage = null; // selected package from form
        private List<ProductSupplierDTO> initialProductsIncluded = new List<ProductSupplierDTO>(); // Products included on package from database
        private List<ProductSupplierDTO>? newSelectedProducts = new List<ProductSupplierDTO>(); // Products included after package update
        private TravelExpertsDataConnection dbConnection = new(); // Class with queries to work with Products, Packages, and Suppliers tables.
        public TravelPackageManager()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                // combo box values displaying package names. Selected Value equals package id.
                cboPackageName.DataSource = db.Packages.ToList();
                cboPackageName.DisplayMember = "PkgName";
                cboPackageName.ValueMember = "PackageID";
                cboPackageName.SelectedValue = cboPackageName.SelectedIndex = 1;

                if (selectedPackage != null)
                {
                    DisplayPackage(); // display the package information on the form
                }

            }
        }

        private void cboProductID_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboPackageName.SelectedValue != null) // if there a package is selected
            {
                // convert the selected Product value to a string
                string PackCodeString = cboPackageName.SelectedValue.ToString()!;

                if (int.TryParse(PackCodeString, out int PackCode))
                {
                    try
                    {
                        using (TravelExpertsContext db = new TravelExpertsContext())
                        {
                            // check if package exists

                            selectedPackage = db.Packages.Find(PackCode); //Find the matching package key from the database
                            if (selectedPackage != null)
                            {
                                DisplayPackage();// if the package code exists, displays the package
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error while retrieving customer data: " + ex.Message,
                            "Database Error");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Unanticipated error: " + ex.Message,
                            ex.GetType().ToString());
                    }
                }
            }
        }

        private void DisplayPackage()
        {

            DateTime? startDateNullable = selectedPackage!.PkgStartDate;
            DateTime? endDateNullable = selectedPackage!.PkgEndDate;
            decimal? commissionNullable = selectedPackage!.PkgAgencyCommission;
            if (selectedPackage != null) // if there is a selected package
            {
                cboPackageName.SelectedItem = selectedPackage.PackageId;
                cboPackageName.Text = selectedPackage.PkgName;

                // Call the method to retrieve data
                List<ProductSupplierDTO> productsAndSuppliers = dbConnection.GetProductsAndSuppliersOfSelectedPackage(selectedPackage);

                // Clear existing rows if needed
                dgvProducts.Rows.Clear();

                // Don't allow manual entries
                this.dgvProducts.AllowUserToAddRows = false;

                // if there are products attached to the package insert data into the data grid view
                // of all product information attached to selected package.
                if (productsAndSuppliers.Count > 0)
                {
                    foreach (ProductSupplierDTO productSupplier in productsAndSuppliers)
                    {
                        // Create a new row and set values in respective columns
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvProducts);

                        row.Cells[0].Value = productSupplier.ProductSupplierId;
                        row.Cells[1].Value = productSupplier.ProductId;
                        row.Cells[2].Value = productSupplier.ProductName;
                        row.Cells[3].Value = productSupplier.SupplierId;
                        row.Cells[4].Value = productSupplier.SupplierName;

                        // Add the row to the DataGridView
                        dgvProducts.Rows.Add(row);
                    }
                }

                txtID.Text = Convert.ToString(selectedPackage.PackageId);
                if (startDateNullable.HasValue)
                {
                    txtStartDate.Text = startDateNullable.Value.ToString("yyyy-MM-dd"); // format start date
                }
                else
                {
                    // default value in the case startDateNullable is null
                    txtStartDate.Text = "N/A";
                }
                if (endDateNullable.HasValue)
                {
                    txtEndDate.Text = endDateNullable.Value.ToString("yyyy-MM-dd"); // format end date
                }
                else
                {
                    // default value in the case endDateNullable is null
                    txtEndDate.Text = "N/A";
                }
                txtDesc.Text = selectedPackage.PkgDesc;
                txtBasePrice.Text = selectedPackage.PkgBasePrice.ToString("c"); // currency format for base price
                if (commissionNullable.HasValue)
                {
                    txtCommission.Text = commissionNullable.Value.ToString("c"); // currency format for commission price
                }
                else
                {
                    // default value in the case commissionNullable is null
                    txtCommission.Text = "N/A";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // display second form to collect data
            frmAddModifyPackage secondForm = new frmAddModifyPackage();

            secondForm.isAdd = true; // it is Add operation
            secondForm.package = null; // open second form with empty text boxes

            DialogResult result = secondForm.ShowDialog();
            if (result == DialogResult.OK) // second form collected data
            {
                // make selected package on this form the one from the other form
                selectedPackage = secondForm.package;
                newSelectedProducts = secondForm.checkedProducts; // Product information of what was selected on the datagridview.
                // add it to the Package table
                try
                {
                    using (TravelExpertsContext db = new TravelExpertsContext())
                    {
                        if (selectedPackage != null)
                        {
                            db.Packages.Add(selectedPackage); // add new package to database
                            db.SaveChanges(); // saves the changes
                            
                            DisplayPackage(); // displays the added package information
                            if (newSelectedProducts != null)
                            {
                                foreach (ProductSupplierDTO product in newSelectedProducts)
                                {
                                    PackagesProductsSupplier update = new PackagesProductsSupplier();
                                    update.ProductSupplierId = product.ProductSupplierId;
                                    update.PackageId = selectedPackage.PackageId;
                                    db.PackagesProductsSuppliers.Add(update);
                                    // Save the changes to the database
                                    db.SaveChanges();
                                }
                            }
                            List<Package> packages = db.Packages.ToList();
                            cboPackageName.DataSource = packages;
                            cboPackageName.DisplayMember = "PckName";
                            cboPackageName.ValueMember = "PackageID";
                            //Since the PackageID is auto genenrated it will always be the last index.
                            cboPackageName.SelectedIndex = packages.Count() - 1;
                        }

                    }
                }
                catch (DbUpdateException ex)
                {
                    string msg = "";
                    var sqlException =
                        (SqlException)ex.InnerException!;
                    foreach (SqlError error in sqlException.Errors)
                    {
                        msg += $"ERROR CODE {error.Number}: {error.Message}\\n";
                    }
                    MessageBox.Show(msg, "Database Error");
                    Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error while adding customer: " + ex.Message,
                        "Database Error");
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Unanticipated error: " + ex.Message,
                        ex.GetType().ToString());
                    Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check if selectedPackage is null
            if (selectedPackage == null)
            {
                MessageBox.Show("Please select a package to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // display second form with current data
            // and collect new data values
            frmAddModifyPackage secondForm = new frmAddModifyPackage();
            secondForm.isAdd = false; // it is Modify operation
            secondForm.package = selectedPackage; // pass selected package to the second form

            DialogResult result = secondForm.ShowDialog();
            if (result == DialogResult.OK) // second form collected new data
            {
                // make selected package on this form the one from the other form
                selectedPackage = secondForm.package;
                // Check if selectedPackage is null after the update
                if (selectedPackage == null)
                {
                    MessageBox.Show("An error occurred while updating the package. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                initialProductsIncluded = secondForm.includedProducts; // Product information of what was there before going through the second form.
                newSelectedProducts = secondForm.checkedProducts; // Product information of what was selected on the datagridview.
                
                // update the selected package data on the Packages table
                try
                {
                    using (TravelExpertsContext db = new TravelExpertsContext())
                    {
                        if (selectedPackage != null)
                        {
                            if (initialProductsIncluded != null)
                            {
                                if (initialProductsIncluded != null && newSelectedProducts != null)
                                {
                                    List<ProductSupplierDTO> newProducts = dbConnection.GetOnlyNewProducts(initialProductsIncluded, newSelectedProducts);
                                    List<ProductSupplierDTO> oldProducts = dbConnection.GetOnlyOldProducts(initialProductsIncluded, newSelectedProducts);
                                    foreach (ProductSupplierDTO product in newProducts)
                                    {
                                        PackagesProductsSupplier update = new PackagesProductsSupplier();
                                        update.ProductSupplierId = product.ProductSupplierId;
                                        update.PackageId = selectedPackage.PackageId;
                                        db.PackagesProductsSuppliers.Add(update);
                                        // Save the changes to the database
                                        db.SaveChanges();
                                    }
                                    foreach( ProductSupplierDTO product in oldProducts)
                                    {
                                        PackagesProductsSupplier update = new PackagesProductsSupplier();
                                        update.ProductSupplierId = product.ProductSupplierId;
                                        update.PackageId = selectedPackage.PackageId;
                                        var entityToRemove = db.PackagesProductsSuppliers.FirstOrDefault(
                                            pps => pps.ProductSupplierId == update.ProductSupplierId && pps.PackageId == update.PackageId);

                                        // Check if the entity exists before attempting to remove it
                                        if (entityToRemove != null)
                                        {
                                            // Remove the entity from the DbSet
                                            db.PackagesProductsSuppliers.Remove(entityToRemove);
                                            // Save the changes to the database
                                            db.SaveChanges();
                                        }

                                    }
                                }
                                else if(initialProductsIncluded == null && newSelectedProducts != null)
                                {
                                    foreach (ProductSupplierDTO product in newSelectedProducts)
                                    {
                                        PackagesProductsSupplier update = new PackagesProductsSupplier();
                                        update.ProductSupplierId = product.ProductSupplierId;
                                        update.PackageId = selectedPackage.PackageId;
                                        db.PackagesProductsSuppliers.Add(update);
                                        // Save the changes to the database
                                        db.SaveChanges();
                                    }
                                }
                            }
                            db.Packages.Update(selectedPackage); // updates the selected package on the database
                            db.SaveChanges(); // saves the changes to the database
                            DisplayPackage(); // displays the modified product
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    string msg = "";
                    var sqlException =
                        (SqlException)ex.InnerException!;
                    foreach (SqlError error in sqlException.Errors)
                    {
                        msg += $"ERROR CODE {error.Number}: {error.Message}\\n";
                    }
                    MessageBox.Show(msg, "Database Error");
                    Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error while modifying customer: " + ex.Message,
                        "Database Error");
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Unanticipated error: " + ex.Message,
                        ex.GetType().ToString());
                    Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // remove the selected package data on the Packages table
            if (selectedPackage != null)
            {
                List<ProductSupplierDTO> products = dbConnection.GetProductsAndSuppliersOfSelectedPackage(selectedPackage);
                // get the user's confirmation
                DialogResult answer = MessageBox.Show(
                    $"Are you sure you want to delete {selectedPackage.PkgName}?",
                    "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );
                if (DialogResult.Yes == answer)
                {
                    try
                    {
                        using (TravelExpertsContext db = new TravelExpertsContext())
                        {
                            List <PackagesProductsSupplier> deletingProducts = new List<PackagesProductsSupplier>();
                            foreach(ProductSupplierDTO product in products)
                            {
                                PackagesProductsSupplier attachedProducts = new PackagesProductsSupplier();
                                attachedProducts.ProductSupplierId = product.ProductSupplierId;
                                attachedProducts.PackageId = selectedPackage.PackageId;
                                var entityToRemove = db.PackagesProductsSuppliers.FirstOrDefault(
                                    pps => pps.ProductSupplierId == attachedProducts.ProductSupplierId && pps.PackageId == attachedProducts.PackageId);
                                // Check if the entity exists before attempting to remove it
                                if (entityToRemove != null)
                                {
                                    // Remove the entity from the DbSet
                                    db.PackagesProductsSuppliers.Remove(entityToRemove);
                                    // Save the changes to the database
                                    db.SaveChanges();
                                }
                            }
                            db.Packages.Remove(selectedPackage); // removes the selected package from the database
                            db.SaveChanges(); // saves the changes to the database
                            cboPackageName.DataSource = db.Packages.ToList(); // updates the packages list after package has been removed
                            // redisplays the updated package codes to the combo box
                            cboPackageName.DisplayMember = "PckName";
                            cboPackageName.ValueMember = "PackageID";
                            selectedPackage = null;
                            ClearControls(); // clears the text boxes
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        string msg = "";
                        var sqlException =
                            (SqlException)ex.InnerException!;
                        foreach (SqlError error in sqlException.Errors)
                        {
                            msg += $"ERROR CODE {error.Number}: {error.Message}\\n";
                        }
                        MessageBox.Show(msg, "Database Error");
                        Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error while deleting customer: " + ex.Message,
                            "Database Error");
                        Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Unanticipated error: " + ex.Message,
                            ex.GetType().ToString());
                        Close();
                    }
                }
            }
        }

        // clears text boxes
        private void ClearControls()
        {
            txtID.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtCommission.Text = string.Empty;
            txtBasePrice.Text = string.Empty;
        }

        // closes this form
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

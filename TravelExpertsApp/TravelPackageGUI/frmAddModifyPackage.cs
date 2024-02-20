using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelPackageData;
using TravelPackageData.Models;

namespace TravelPackageGUI
{
    public partial class frmAddModifyPackage : Form
    {
        public bool isAdd; // true when Add, and false when Modify
        public Package? package; // new package data
        public List<ProductSupplierDTO> checkedProducts = new List<ProductSupplierDTO>(); // list of products checked off the dgv
        public List<ProductSupplierDTO> includedProducts = new List<ProductSupplierDTO>(); // list of products that were already included
        public List<ProductSupplierDTO> currentlyCheckedProducts = new List<ProductSupplierDTO>();
        private TravelExpertsDataConnection dbConnection = new();

        public frmAddModifyPackage()
        {
            {
                InitializeComponent();
            }
        }
        private void frmAddModifyPackage_Load(object sender, EventArgs e)
        {
            if (isAdd) // if the user selects the add button
            {
                pnlHider.Visible = true;
                Text = "Add Package";
                DisplayProducts();
            }
            else // if the user selects the modify button
            {
                pnlHider.Visible = false;
                Text = "Modify Package";
                DisplayPackage();
            }
        }
        private void DisplayProducts()
        {
            List<ProductSupplierDTO> allProductsAndSuppliers = dbConnection.GetAllProductsAndSuppliers();
            if (package == null)
            {
                // add all avaliable products to the datagrid view
                if (allProductsAndSuppliers != null)
                {
                    foreach (ProductSupplierDTO product in allProductsAndSuppliers)
                    {
                        // Create a new row and set values in respective columns
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvProducts);

                        row.Cells[0].Value = product.ProductSupplierId;
                        row.Cells[1].Value = product.ProductId;
                        row.Cells[2].Value = product.ProductName;
                        row.Cells[3].Value = product.SupplierId;
                        row.Cells[4].Value = product.SupplierName;

                        // Add the row to the DataGridView
                        dgvProducts.Rows.Add(row);
                    }
                }
            }
        }


        private void DisplayPackage()
        {
            DateTime? startDateNullable = package!.PkgStartDate;
            DateTime? endDateNullable = package!.PkgEndDate;
            decimal? commissionNullable = package!.PkgAgencyCommission;
            List<ProductSupplierDTO> allProductsAndSuppliers = dbConnection.GetAllProductsAndSuppliers(); // list of all products and suppliers

            if (package != null)
            {
                // Call the method to retrieve data
                List<ProductSupplierDTO> productsAndSuppliers = dbConnection.GetProductsAndSuppliersOfSelectedPackage(package);

                // Clear existing rows and items if needed
                dgvProducts.Rows.Clear();
                lstProducts.Items.Clear();

                // Don't allow manual entries
                this.dgvProducts.AllowUserToAddRows = false;

                // If there are products attached to the package, add them to the list box.
                if (productsAndSuppliers != null)
                {
                    foreach (ProductSupplierDTO product in productsAndSuppliers)
                    {
                        lstProducts.Items.Add(product);
                    }
                }

                // add all avaliable products to the datagrid view
                if (allProductsAndSuppliers != null)
                {
                    foreach (ProductSupplierDTO product in allProductsAndSuppliers)
                    {
                        // Create a new row and set values in respective columns
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvProducts);

                        // Assuming the order of columns is the same as you added them
                        row.Cells[0].Value = product.ProductSupplierId;
                        row.Cells[1].Value = product.ProductId;
                        row.Cells[2].Value = product.ProductName;
                        row.Cells[3].Value = product.SupplierId;
                        row.Cells[4].Value = product.SupplierName;

                        // Add the row to the DataGridView
                        dgvProducts.Rows.Add(row);

                        // check the box if the product off the datagrid view if the product is already added   
                        if (allProductsAndSuppliers != null && productsAndSuppliers != null)
                        {
                            if (productsAndSuppliers.Any(x => x.ProductId == product.ProductId && x.SupplierId == product.SupplierId))
                            {
                                row.Cells[5].Value = true;
                            }
                            else
                            {
                                row.Cells[5].Value = false;
                            }
                        }
                    }
                }

                txtPackageID.Text = package.PackageId.ToString();
                txtName.Text = package.PkgName;
                if (startDateNullable.HasValue)
                {
                    txtStartDate.Text = startDateNullable.Value.ToString("yyyy-MM-dd"); // format date string
                }
                else
                {
                    // Default value in the case startDateNullable is null
                    txtStartDate.Text = "N/A"; 
                }
                if (endDateNullable.HasValue)
                {
                    txtEndDate.Text = endDateNullable.Value.ToString("yyyy-MM-dd"); // format date string
                }
                else
                {
                    // Default value in the case endDateNullable is null
                    txtEndDate.Text = "N/A";
                }
                txtDesc.Text = package.PkgDesc;
                txtPrice.Text = package.PkgBasePrice.ToString("c"); // currency format base price
                if (commissionNullable.HasValue)
                {
                    txtCommission.Text = commissionNullable.Value.ToString("c"); // currency format commission price
                }
                else
                {
                    // Default value in the case commissionNullable is null
                    txtCommission.Text = "N/A";
                }
            }
        }

        // Saves the new package to the data base if all entries are validated
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtName) &&
                Validator.IsPresent(txtDesc) &&
                Validator.IsPresent(txtPrice) &&
                Validator.IsValidDate(txtStartDate) &&
                Validator.IsValidDate(txtEndDate) &&
                Validator.IsValidEndDate(txtEndDate, txtStartDate) &&
                Validator.IsNonNegativeDecimal(txtPrice) &&
                Validator.IsNonNegativeDecimal(txtCommission) &&
                Validator.IsValidCommission(txtCommission, txtPrice)
                )
            {
                if (isAdd)
                {
                    package = new Package(); // creates an empty Customer object
                    GetPackageData(); // gathers product data to enter into the database
                }
                else // Modify
                {
                    GetPackageData(); // gathers product data to enter into the database
                }

                DialogResult = DialogResult.OK; // closes the form
            }
        }

        // Displays package data if package is not null
        private void GetPackageData()
        {

            if (package != null)
            {
                includedProducts = dbConnection.GetProductsAndSuppliersOfSelectedPackage(package);
                package.PkgName = txtName.Text;
                package.PkgDesc = txtDesc.Text;

                // format package data string values for user friendly display
                if (!String.IsNullOrEmpty(txtStartDate.Text) && txtStartDate.Text != "N/A")
                {
                    package.PkgStartDate = Convert.ToDateTime(txtStartDate.Text);
                }
                if (!String.IsNullOrEmpty(txtEndDate.Text) && txtEndDate.Text != "N/A")
                {
                    package.PkgEndDate = Convert.ToDateTime(txtEndDate.Text);
                }
                if (!String.IsNullOrEmpty(txtPrice.Text))
                {
                    package.PkgBasePrice = Convert.ToDecimal(txtPrice.Text.Replace("$", "").Replace(",", ""));
                }
                if (!String.IsNullOrEmpty(txtCommission.Text))
                {
                    package.PkgAgencyCommission = Convert.ToDecimal(txtCommission.Text.Replace("$", "").Replace(",", ""));
                }

                // check through each row in the data grid view
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[5]; // the checkbox cell on the datagrid view
                    if (chk.Value != null && (bool)chk.Value == true) // if the checkbox is checked
                    {
                        int prodSupId = Convert.ToInt32(row.Cells[0].Value);
                        int productId = Convert.ToInt32(row.Cells[1].Value);
                        string productName = row.Cells[2].Value.ToString();
                        int supplierId = Convert.ToInt32(row.Cells[3].Value);
                        string supplierName = row.Cells[4].Value.ToString();

                        // Create a new ProductSupplierDTO object with updated values
                        ProductSupplierDTO selectedProduct = new ProductSupplierDTO(prodSupId, productId, productName, supplierId, supplierName);
                        // add the new ProductSupplierDTO object to the products list.
                        checkedProducts.Add(selectedProduct);
                    }
                }
            }
        }

        // button to close this form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

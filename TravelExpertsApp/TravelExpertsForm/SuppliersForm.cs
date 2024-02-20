using TravelExpertsSuppliersDB;
using TravelExpertsSuppliersDB.Models;

namespace TravelExpertsForm
{
    public partial class SuppliersForm : Form
    {
        private Supplier? selectedSupplier = null!;

        public SuppliersForm()
        {
            InitializeComponent();
        }

        private void SuppliersForm_Load(object sender, EventArgs e)
        {
            DisplaySuppliers();
        }

        /// <summary>
        /// Fills the data grid view with data from the suppliers table
        /// </summary>
        private void DisplaySuppliers()  // Refreshes the Suppliers
        {
            dgvSuppliers.Columns.Clear(); // Clear any existing data.
            dgvSuppliers.DataSource = TravelExpertsDataAccess.GetAllSuppliers();  // Pulls a formatted list of data from the DB

            // add column for modify button
            DataGridViewButtonColumn modifyColumn = new()
            {
                UseColumnTextForButtonValue = true,
                HeaderText = "",
                Text = "Edit"
            };
            dgvSuppliers.Columns.Add(modifyColumn);


            //// Removed Delete column. Deleting suppliers is problematic if it's linked to any other data in the database, which is very likely

            //// add column for delete button
            //DataGridViewButtonColumn deleteColumn = new()
            //{
            //    UseColumnTextForButtonValue = true,
            //    HeaderText = "",
            //    Text = "Delete"
            //};
            //dgvSuppliers.Columns.Add(deleteColumn);


            dgvSuppliers.Columns[0].HeaderText = "ID";
            dgvSuppliers.Columns[0].Width = 60;
            dgvSuppliers.Columns[1].HeaderText = "Name";
            dgvSuppliers.Columns[1].Width = 200;
            dgvSuppliers.Columns[2].HeaderText = "Contacts";

            // format the column header
            dgvSuppliers.EnableHeadersVisualStyles = false;
            dgvSuppliers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvSuppliers.ColumnHeadersDefaultCellStyle.BackColor = Color.Goldenrod;
            dgvSuppliers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // format the odd numbered rows
            dgvSuppliers.AlternatingRowsDefaultCellStyle.BackColor = Color.PaleGoldenrod;


        }

        /// <summary>
        /// when the data grid view is clicked, check if its over the modify button and call modify if it is
        /// </summary>
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // index values for Modify and Delete button columns
            const int ModifyIndex = 3;
            //const int DeleteIndex = 4;

            if (e.RowIndex > -1)  // make sure header row wasn't clicked
            {
                if (e.ColumnIndex == ModifyIndex /*|| e.ColumnIndex == DeleteIndex*/)
                {
                    DataGridViewCell cell = dgvSuppliers.Rows[e.RowIndex].Cells[0];
                    string? val = cell.Value.ToString();
                    if (val != null)
                    {
                        int supplierId = Int32.Parse(val);
                        selectedSupplier = TravelExpertsDataAccess.FindSupplier(supplierId);
                    }


                }

                if (selectedSupplier != null)
                {
                    if (e.ColumnIndex == ModifyIndex)
                    {
                        ModifySupplier();
                    }
                    //else if (e.ColumnIndex == DeleteIndex)
                    //{
                    //    DeleteSupplier();
                    //}
                }
            }
        }


        // Removed DeleteSupplier() Deleting suppliers is problematic if it's linked to any other data in the database, which is very likely
        //private void DeleteSupplier()
        //{
        //    if (selectedSupplier != null)
        //    {
        //        DialogResult result =
        //        MessageBox.Show($"Delete {selectedSupplier.SupName}?",
        //        "Confirm Delete", MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question);
        //        if (result == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                TravelExpertsDataAccess.RemoveSupplier(selectedSupplier);
        //                DisplaySuppliers();
        //            }
        //            catch (DataAccessException ex)
        //            {
        //                HandleDataAccessError(ex);
        //            }
        //        }
        //    }

        //}
        /// <summary>
        /// Open a dialog for modifying the selected supplier. If it results in OK, save the change into the DB.
        /// </summary>
        private void ModifySupplier()
        {
            if (selectedSupplier != null)
            {
                AddModifySupplierForm addModifyForm = new()
                {
                    supplier = selectedSupplier
                };
                DialogResult result = addModifyForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    try
                    {
                        selectedSupplier = addModifyForm.supplier;
                        TravelExpertsDataAccess.UpdateSupplier(selectedSupplier);
                        DisplaySuppliers();
                    }

                    catch (DataAccessException ex)
                    {
                        HandleDataAccessError(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Open a dialog for addiing a new supplier to the DB.
        /// </summary>
        private void AddSupplier()
        {
            AddModifySupplierForm addModifyForm = new();
            DialogResult result = addModifyForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    selectedSupplier = addModifyForm.supplier;
                    TravelExpertsDataAccess.UpdateSupplier(selectedSupplier);
                    DisplaySuppliers();
                }

                catch (DataAccessException ex)
                {
                    HandleDataAccessError(ex);
                }
            }
        }

        private void HandleDataAccessError(DataAccessException ex)
        {
            // if concurrency conflict, re-display products
            if (ex.IsConcurrencyError)
            {
                DisplaySuppliers();
            }

            // display error message in dialog with error type as title
            MessageBox.Show(ex.Message, ex.ErrorType);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSupplier();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

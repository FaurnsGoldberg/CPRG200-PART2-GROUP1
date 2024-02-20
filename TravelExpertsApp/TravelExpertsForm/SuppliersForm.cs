using TravelExpertsSuppliersDB;
using TravelExpertsSuppliersDB.Models;

namespace TravelExpertsForm
{
    public partial class SuppliersForm : Form
    {
        private Supplier? selectedSupplier = null!; // Which product is currently being modified/deleted
        public SuppliersForm()
        {
            InitializeComponent();
        }

        private void SuppliersForm_Load(object sender, EventArgs e)
        {
            DisplaySuppliers();
        }

        private void DisplaySuppliers()  // Refreshes the Suppliers
        {
            dgvSuppliers.Columns.Clear(); // Clear any existing data.
            dgvSuppliers.DataSource = TravelExpertsDataAccess.GetAllSuppliers();  // Pulls a formatted list of data from the DB

            // add column for modify button
            DataGridViewButtonColumn modifyColumn = new()
            {
                UseColumnTextForButtonValue = true,
                HeaderText = "",
                Text = "Modify"
            };
            dgvSuppliers.Columns.Add(modifyColumn);

            // add column for delete button
            DataGridViewButtonColumn deleteColumn = new()
            {
                UseColumnTextForButtonValue = true,
                HeaderText = "",
                Text = "Remove"
            };
            dgvSuppliers.Columns.Add(deleteColumn);
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

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // index values for Modify and Delete button columns
            const int ModifyIndex = 3;
            const int DeleteIndex = 4;

            if (e.RowIndex > -1)  // make sure header row wasn't clicked
            {
                if (e.ColumnIndex == ModifyIndex || e.ColumnIndex == DeleteIndex)
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
                    else if (e.ColumnIndex == DeleteIndex)
                    {
                        DeleteSupplier();
                    }
                }
            }
        }

        private void DeleteSupplier()
        {
            if (selectedSupplier != null)
            {
                DialogResult result =
                MessageBox.Show($"Delete {selectedSupplier.SupName}?",
                "Confirm Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        TravelExpertsDataAccess.RemoveSupplier(selectedSupplier);
                        DisplaySuppliers();
                    }
                    catch (DataAccessException ex)
                    {
                        HandleDataAccessError(ex);
                    }
                }
            }

        }

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

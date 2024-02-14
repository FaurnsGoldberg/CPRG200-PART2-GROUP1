using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workshop4;
using TravelPackageGUI;
using ThreadedWorkshop4;

namespace TravelExpertsForm
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SuppliersForm SuppliersForm = new SuppliersForm();
            SuppliersForm.FormClosed += new FormClosedEventHandler(CloseForm);
            this.Hide();
            SuppliersForm.ShowDialog();
        }

        private void btnProds_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.FormClosed += new FormClosedEventHandler(CloseForm);
            this.Hide();
            products.ShowDialog();
        }

        private void btnPackage_Click(object sender, EventArgs e)
        {
            TravelPackageManager travelPackageManager = new TravelPackageManager();
            travelPackageManager.FormClosed += new FormClosedEventHandler(CloseForm);
            this.Hide();
            travelPackageManager.ShowDialog();
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnAgents_Click(object sender, EventArgs e)
        {
            AgentsForm agentsForm = new AgentsForm();
            agentsForm.Show();

            agentsForm.FormClosed += new FormClosedEventHandler(CloseForm);

        }
    }
}
namespace TravelExpertsForm
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            btnPackages = new Button();
            btnSuppliers = new Button();
            btnPackage = new Button();
            pictureBox1 = new PictureBox();
            btnAgents = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnPackages
            // 
            btnPackages.Location = new Point(25, 292);
            btnPackages.Name = "btnPackages";
            btnPackages.Size = new Size(144, 70);
            btnPackages.TabIndex = 0;
            btnPackages.Text = "&Products";
            btnPackages.UseVisualStyleBackColor = true;
            btnPackages.Click += btnProds_Click;
            // 
            // btnSuppliers
            // 
            btnSuppliers.Location = new Point(388, 292);
            btnSuppliers.Name = "btnSuppliers";
            btnSuppliers.Size = new Size(180, 70);
            btnSuppliers.TabIndex = 1;
            btnSuppliers.Text = "&Suppliers";
            btnSuppliers.UseVisualStyleBackColor = true;
            btnSuppliers.Click += btnSuppliers_Click;
            // 
            // btnPackage
            // 
            btnPackage.Location = new Point(190, 292);
            btnPackage.Name = "btnPackage";
            btnPackage.Size = new Size(170, 70);
            btnPackage.TabIndex = 2;
            btnPackage.Text = "Pa&ckages";
            btnPackage.UseVisualStyleBackColor = true;
            btnPackage.Click += btnPackage_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(273, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(283, 258);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnAgents
            // 
            btnAgents.Location = new Point(594, 292);
            btnAgents.Name = "btnAgents";
            btnAgents.Size = new Size(180, 70);
            btnAgents.TabIndex = 4;
            btnAgents.Text = "&Agents";
            btnAgents.UseVisualStyleBackColor = true;
            btnAgents.Click += btnAgents_Click;
            // 
            // Welcome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RoyalBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAgents);
            Controls.Add(pictureBox1);
            Controls.Add(btnPackage);
            Controls.Add(btnSuppliers);
            Controls.Add(btnPackages);
            Name = "Welcome";
            Text = "Welcome";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnPackages;
        private Button btnSuppliers;
        private Button btnPackage;
        private PictureBox pictureBox1;
        private Button btnAgents;
    }
}
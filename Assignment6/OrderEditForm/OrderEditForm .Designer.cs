namespace OrderEditForm
{
    partial class OrderEditForm 
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
            txtOrderId = new TextBox();
            txtCustomer = new TextBox();
            dgvEditDetails = new DataGridView();
            btnAddDetail = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEditDetails).BeginInit();
            SuspendLayout();
            // 
            // txtOrderId
            // 
            txtOrderId.Location = new Point(68, 77);
            txtOrderId.Name = "txtOrderId";
            txtOrderId.Size = new Size(100, 23);
            txtOrderId.TabIndex = 0;
            // 
            // txtCustomer
            // 
            txtCustomer.Location = new Point(208, 77);
            txtCustomer.Name = "txtCustomer";
            txtCustomer.Size = new Size(100, 23);
            txtCustomer.TabIndex = 1;
            // 
            // dgvEditDetails
            // 
            dgvEditDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEditDetails.Location = new Point(68, 106);
            dgvEditDetails.Name = "dgvEditDetails";
            dgvEditDetails.Size = new Size(240, 150);
            dgvEditDetails.TabIndex = 2;
            // 
            // btnAddDetail
            // 
            btnAddDetail.Location = new Point(68, 304);
            btnAddDetail.Name = "btnAddDetail";
            btnAddDetail.Size = new Size(75, 23);
            btnAddDetail.TabIndex = 3;
            btnAddDetail.Text = "Add";
            btnAddDetail.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Location = new Point(233, 304);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(btnAddDetail);
            Controls.Add(dgvEditDetails);
            Controls.Add(txtCustomer);
            Controls.Add(txtOrderId);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvEditDetails).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtOrderId;
        public TextBox txtCustomer;
        private DataGridView dgvEditDetails;
        private Button btnAddDetail;
        private Button btnSave;
    }
}

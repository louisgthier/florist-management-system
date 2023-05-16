namespace ManagerGUI
{
    partial class Form1
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
            clients = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)clients).BeginInit();
            SuspendLayout();
            // 
            // clients
            // 
            clients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            clients.Location = new Point(120, 41);
            clients.Name = "clients";
            clients.RowHeadersWidth = 51;
            clients.RowTemplate.Height = 29;
            clients.Size = new Size(275, 138);
            clients.TabIndex = 0;
            clients.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(clients);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)clients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView clients;
    }
}
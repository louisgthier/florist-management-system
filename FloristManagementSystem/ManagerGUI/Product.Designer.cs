namespace ManagerGUI
{
    partial class Product
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
            ProductTable = new DataGridView();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)ProductTable).BeginInit();
            SuspendLayout();
            // 
            // ProductTable
            // 
            ProductTable.AllowUserToAddRows = false;
            ProductTable.AllowUserToDeleteRows = false;
            ProductTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ProductTable.Location = new Point(12, 12);
            ProductTable.Name = "ProductTable";
            ProductTable.ReadOnly = true;
            ProductTable.RowHeadersWidth = 51;
            ProductTable.RowTemplate.Height = 29;
            ProductTable.Size = new Size(555, 504);
            ProductTable.TabIndex = 0;
            ProductTable.CellContentClick += ProductTable_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(673, 101);
            button1.Name = "button1";
            button1.Size = new Size(159, 29);
            button1.TabIndex = 1;
            button1.Text = "Retour au menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Product
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 528);
            Controls.Add(button1);
            Controls.Add(ProductTable);
            Name = "Product";
            Text = "Product";
            Load += Product_Load;
            ((System.ComponentModel.ISupportInitialize)ProductTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView ProductTable;
        private Button button1;
    }
}
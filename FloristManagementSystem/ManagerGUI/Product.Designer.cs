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
            ((System.ComponentModel.ISupportInitialize)ProductTable).BeginInit();
            SuspendLayout();
            // 
            // ProductTable
            // 
            ProductTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ProductTable.Location = new Point(12, 12);
            ProductTable.Name = "ProductTable";
            ProductTable.RowHeadersWidth = 51;
            ProductTable.RowTemplate.Height = 29;
            ProductTable.Size = new Size(555, 504);
            ProductTable.TabIndex = 0;
            ProductTable.CellContentClick += ProductTable_CellContentClick;
            // 
            // Product
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 528);
            Controls.Add(ProductTable);
            Name = "Product";
            Text = "Product";
            Load += Product_Load;
            ((System.ComponentModel.ISupportInitialize)ProductTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView ProductTable;
    }
}
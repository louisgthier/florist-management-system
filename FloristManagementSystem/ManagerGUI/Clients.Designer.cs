namespace ManagerGUI
{
    partial class Clients
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
            dataGridView1 = new DataGridView();
            button1 = new Button();
            export_xml_button = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1, -1);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1092, 489);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // button1
            // 
            button1.Location = new Point(1131, 378);
            button1.Name = "button1";
            button1.Size = new Size(109, 29);
            button1.TabIndex = 1;
            button1.Text = "retour menu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // export_xml_button
            // 
            export_xml_button.Location = new Point(1131, 216);
            export_xml_button.Name = "export_xml_button";
            export_xml_button.Size = new Size(109, 29);
            export_xml_button.TabIndex = 2;
            export_xml_button.Text = "Export XML";
            export_xml_button.UseVisualStyleBackColor = true;
            export_xml_button.Click += export_xml_button_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1131, 290);
            button2.Name = "button2";
            button2.Size = new Size(109, 29);
            button2.TabIndex = 3;
            button2.Text = "Export Json";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1146, 193);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 4;
            label1.Text = "client actif";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1139, 263);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 5;
            label2.Text = "client inactif";
            // 
            // Clients
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 487);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(export_xml_button);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Clients";
            Text = "Clients";
            Load += Clients_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button export_xml_button;
        private Button button2;
        private Label label1;
        private Label label2;
    }
}
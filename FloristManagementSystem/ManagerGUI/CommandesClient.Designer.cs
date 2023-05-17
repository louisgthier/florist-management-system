namespace ManagerGUI
{
    partial class CommandesClient
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
            dataGridView2 = new DataGridView();
            button1 = new Button();
            VINV = new Button();
            CC = new Button();
            CPAV = new Button();
            CAL = new Button();
            CL = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(1, -1);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1218, 490);
            dataGridView2.TabIndex = 0;
            dataGridView2.CellClick += dataGridView2_CellClick_1;
            // 
            // button1
            // 
            button1.Location = new Point(1237, 411);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Retour client";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // VINV
            // 
            VINV.Location = new Point(1237, 115);
            VINV.Name = "VINV";
            VINV.Size = new Size(94, 29);
            VINV.TabIndex = 2;
            VINV.Text = "VINV";
            VINV.UseVisualStyleBackColor = true;
            VINV.Click += VINV_Click;
            // 
            // CC
            // 
            CC.Location = new Point(1237, 160);
            CC.Name = "CC";
            CC.Size = new Size(94, 29);
            CC.TabIndex = 3;
            CC.Text = "CC";
            CC.UseVisualStyleBackColor = true;
            CC.Click += CC_Click;
            // 
            // CPAV
            // 
            CPAV.Location = new Point(1237, 207);
            CPAV.Name = "CPAV";
            CPAV.Size = new Size(94, 29);
            CPAV.TabIndex = 4;
            CPAV.Text = "CPAV";
            CPAV.UseVisualStyleBackColor = true;
            CPAV.Click += CPAV_Click;
            // 
            // CAL
            // 
            CAL.Location = new Point(1237, 255);
            CAL.Name = "CAL";
            CAL.Size = new Size(94, 29);
            CAL.TabIndex = 5;
            CAL.Text = "CAL";
            CAL.UseVisualStyleBackColor = true;
            CAL.Click += CAL_Click;
            // 
            // CL
            // 
            CL.Location = new Point(1237, 304);
            CL.Name = "CL";
            CL.Size = new Size(94, 29);
            CL.TabIndex = 6;
            CL.Text = "CL";
            CL.UseVisualStyleBackColor = true;
            CL.Click += CL_Click;
            // 
            // CommandesClient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1370, 490);
            Controls.Add(CL);
            Controls.Add(CAL);
            Controls.Add(CPAV);
            Controls.Add(CC);
            Controls.Add(VINV);
            Controls.Add(button1);
            Controls.Add(dataGridView2);
            Name = "CommandesClient";
            Text = "Form1";
            Load += CommandesClient_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView2;
        private Button button1;
        private Button VINV;
        private Button CC;
        private Button CPAV;
        private Button CAL;
        private Button CL;
    }
}
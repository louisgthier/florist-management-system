using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilityLibrary;

namespace ManagerGUI
{
    public partial class Statistics : Form
    {
        private ManagerMenu managerForm;
        public Statistics(ManagerMenu managerForm)
        {
            InitializeComponent();
            this.managerForm = managerForm;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            managerForm.Show();
            this.Close();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "client du mois";
            dataGridView1.Columns[1].Name = "client de l'année";
            dataGridView1.Columns[2].Name = "prix moyen bouquet";
            dataGridView1.Columns[3].Name = "meilleur bouquet";
            dataGridView1.Columns[4].Name = "magasin avec le plus gros CA";
            dataGridView1.Columns[5].Name = "fleur la moins vendu";
            dataGridView1.Dock = DockStyle.Fill; // Ajuste le contrôle pour remplir tout l'espace du formulaire

            for (int i = 0; i < 6; i++) {
                dataGridView1.Rows[0].Cells[i].Value= MySQLUtil.Statistics(i); 
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

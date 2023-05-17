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

            label1.Text = "Statistiques:\n\n";

            label1.Text += "Prix moyen tous bouquets confondus: " + MySQLUtil.GetStatistics(0);
            label1.Text += "\n\nClient du mois: " + MySQLUtil.GetStatistics(1);
            label1.Text += "\n\nClient de l'année: " + MySQLUtil.GetStatistics(2);
            label1.Text += "\n\nBouquet standard le plus vendu: " + MySQLUtil.GetStatistics(3);
            label1.Text += "\n\nMagasin avec le plus gros chiffre d'affaires: " + MySQLUtil.GetStatistics(4);
            label1.Text += "\n\nFleur la moins vendue: " + MySQLUtil.GetStatistics(5);


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

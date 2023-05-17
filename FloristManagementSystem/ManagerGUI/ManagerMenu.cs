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
    public partial class ManagerMenu : Form
    {
        public ManagerMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Création de la deuxième page
            Clients form3 = new Clients(this);
            form3.Show();
            this.Hide();


        }
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Création de la deuxième page
            //ManagerMenu commandes = new ManagerMenu();

            List<PurchaseOrder> commandesClient = MySQLUtil.GetPurchaseOrders();
            // Créez et affichez le formulaire contenant les commandes du client
            CommandesClient commandesClientForm = new CommandesClient(commandesClient, this);
            commandesClientForm.Show();
            this.Hide();

            /*commandes.Show();
            this.Hide();*/
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Création de la deuxième page
            Statistics form1 = new Statistics(this);
            form1.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Création de la deuxième page
            Product form4 = new Product();
            form4.Show();
            this.Hide();
        }
        
    }
}

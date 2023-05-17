using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using UtilityLibrary;

namespace ManagerGUI
{
    public partial class Clients : Form
    {
        private ManagerMenu managerForm;
        public Clients(ManagerMenu managerForm)
        {
            InitializeComponent();
            this.managerForm = managerForm;
        }

        List<Client> listClients = new List<Client>();

        private void Clients_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "id";
            dataGridView1.Columns[1].Name = "first_name";
            dataGridView1.Columns[2].Name = "name";
            dataGridView1.Columns[3].Name = "fidelité";
            dataGridView1.Columns[4].Name = "email";
            dataGridView1.Columns[5].Name = "phone_number";
            dataGridView1.Columns[6].Name = "address";
            dataGridView1.Columns[7].Name = "inscription_date";

            dataGridView1.Dock = DockStyle.Fill; // Ajuste le contrôle pour remplir tout l'espace du formulaire

            listClients = MySQLUtil.GetClients();

            foreach (Client client in listClients)
            {
                dataGridView1.Rows.Add(new string[] { client.Id.ToString(), client.FirstName.ToString(), client.LastName.ToString(), "aucun", client.Email.ToString(), client.PhoneNumber.ToString(), client.Address.ToString(), client.InscriptionDate.ToString() });
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int clientIndex = e.RowIndex;
                Client selectedClient = listClients[clientIndex];
                // Obtenez les commandes du client en utilisant la méthode appropriée de MySQLUtil
                List<PurchaseOrder> commandesClient = MySQLUtil.GetPurchaseOrders(selectedClient.Id);
                // Créez et affichez le formulaire contenant les commandes du client
                CommandesClient commandesClientForm = new CommandesClient(commandesClient, this);
                commandesClientForm.Show();
                this.Hide();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            managerForm.Show();
            this.Close();
        }
    }
}

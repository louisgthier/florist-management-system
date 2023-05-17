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
    public partial class CommandesClient : Form
    {
        private Form clientsForm;
        List<PurchaseOrder> commandesClients = new List<PurchaseOrder>();
        int selectedOrderIndex = 0;

        public CommandesClient(List<PurchaseOrder> commandesClients, Form clientsForm)
        {
            InitializeComponent();
            this.commandesClients = commandesClients;
            this.clientsForm = clientsForm;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CommandesClient_Load(object sender, EventArgs e)
        {
            dataGridView2.ColumnCount = 9;
            dataGridView2.Columns[0].Name = "id";
            dataGridView2.Columns[1].Name = "delivery_address";
            dataGridView2.Columns[2].Name = "message";
            dataGridView2.Columns[3].Name = "delivery_date";
            dataGridView2.Columns[4].Name = "order_date";
            dataGridView2.Columns[5].Name = "order_state";
            dataGridView2.Columns[6].Name = "arrangement_id";
            dataGridView2.Columns[7].Name = "bouquet_name";
            dataGridView2.Columns[8].Name = "shop_id";
            dataGridView2.Dock = DockStyle.Fill; // Ajuste le contrôle pour remplir tout l'espace du formulaire

            foreach (PurchaseOrder commande in commandesClients)
            {

                dataGridView2.Rows.Add(new string[]
                {
                    commande.Id.ToString(), commande.DeliveryAddress.ToString(), commande.Message.ToString(),commande.DeliveryDate.ToString(),commande.OrderDate.ToString(),
                    commande.OrderState.ToString(), commande.ArrangementId.ToString() ,commande.BouquetName is null ? "" : commande.BouquetName.ToString(),commande.ShopId.ToString()
                });
            }
            selectedOrderIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            clientsForm.Show();
            this.Close();
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               selectedOrderIndex = e.RowIndex;

            }
        }

        private void VINV_Click(object sender, EventArgs e)
        {
            if (selectedOrderIndex >= 0)
            {
                MySQLUtil.UpdateOrderState(commandesClients[selectedOrderIndex].Id, "VINV");
                dataGridView2.Rows[selectedOrderIndex].Cells[5].Value = "VINV";
            }
        }

        private void CC_Click(object sender, EventArgs e)
        {
            if (selectedOrderIndex >= 0)
            {
                MySQLUtil.UpdateOrderState(commandesClients[selectedOrderIndex].Id, "CC");
                dataGridView2.Rows[selectedOrderIndex].Cells[5].Value = "CC";
            }

        }

        private void CPAV_Click(object sender, EventArgs e)
        {
            if (selectedOrderIndex >= 0)
            {
                MySQLUtil.UpdateOrderState(commandesClients[selectedOrderIndex].Id, "CPAV");
                dataGridView2.Rows[selectedOrderIndex].Cells[5].Value = "CPAV";
            }

        }

        private void CAL_Click(object sender, EventArgs e)
        {
            if (selectedOrderIndex >= 0)
            {
                MySQLUtil.UpdateOrderState(commandesClients[selectedOrderIndex].Id, "CAL");
                dataGridView2.Rows[selectedOrderIndex].Cells[5].Value = "CAL";
            }
        }

        private void CL_Click(object sender, EventArgs e)
        {
            if (selectedOrderIndex >= 0)
            {
                MySQLUtil.UpdateOrderState(commandesClients[selectedOrderIndex].Id, "CL");
                dataGridView2.Rows[selectedOrderIndex].Cells[5].Value = "CL";
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UtilityLibrary;

namespace ManagerGUI
{
    public partial class Product : Form
    {
        List<Item> listProducts = new List<Item>();
        public Product()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            ProductTable.ColumnCount = 4;
            ProductTable.Columns[0].Name = "name";
            ProductTable.Columns[1].Name = "price";
            ProductTable.Columns[2].Name = "type";
            ProductTable.Columns[3].Name = "availability";
            ProductTable.Dock = DockStyle.Fill; // Ajuste le contrôle pour remplir tout l'espace du formulaire

            listProducts = MySQLUtil.GetItems();

            foreach (Item product in listProducts)
            {
                ProductTable.Rows.Add(new string[] { product.Name.ToString(), product.Price.ToString(), product.Type.ToString(), product.Availability.ToString() }); 
            }

        }

        private void ProductTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

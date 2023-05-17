using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using UtilityLibrary;
using MySqlX.XDevAPI.Common;
using System.Xml;
using System.Linq.Expressions;

namespace UtilityLibrary
{
    public static partial class MySQLUtil
    {
        public static List<Client> GetClients()
        {
            List<Client> result = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Create a MySqlCommand object
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM client;";


                    MySqlDataReader reader;
                    reader = command.ExecuteReader();

                    result = new List<Client>();


                    while (reader.Read())// parcourt ligne par ligne
                    {
                        result.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetString(4),
                            reader.GetString(5), reader.GetString(6), reader.GetDateTime(7)));
                    }
                }
            }

            return result;
        }
        public static void UpdateOrderState(int orderId, string orderState)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();


                string updateQuery = "UPDATE purchase_order SET order_state = @NewState WHERE purchase_order.id=@OrderId";

                // Créez une commande SQL avec la requête et la connexion associées
                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewState", orderState);
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public static void ExportClientsToXML()
        {
            // SQL query to retrieve clients who have ordered more than once in the last month
            string sqlQuery = @"
                SELECT c.id, c.first_name, c.name, c.email, c.phone_number, c.address, c.credit_card, c.inscription_date
                FROM client c
                WHERE c.id IN (
                    SELECT p.client_id
                    FROM purchase_order p
                    WHERE p.order_date >= DATE_SUB(NOW(), INTERVAL 1 MONTH)
                    GROUP BY p.client_id
                    HAVING COUNT(*) > 1
                )";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the database connection
                connection.Open();

                // Create a MySqlCommand object
                using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                {
                    // Create a DataTable to hold the result set
                    DataTable dataTable = new DataTable();

                    // Execute the query and fill the DataTable
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Create an XmlDocument to hold the XML data
                    XmlDocument xmlDocument = new XmlDocument();

                    // Create the root element
                    XmlElement rootElement = xmlDocument.CreateElement("Clients");
                    xmlDocument.AppendChild(rootElement);

                    // Iterate through the rows in the DataTable
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Create a client element
                        XmlElement clientElement = xmlDocument.CreateElement("Client");

                        // Iterate through the columns in the row
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            // Create an element for each column and set its value
                            XmlElement columnElement = xmlDocument.CreateElement(column.ColumnName);
                            columnElement.InnerText = row[column].ToString();

                            // Add the column element to the client element
                            clientElement.AppendChild(columnElement);
                        }

                        // Add the client element to the root element
                        rootElement.AppendChild(clientElement);
                    }

                    // Save the XML document to a file or perform any desired operations
                    xmlDocument.Save("clients.xml");
                }
            }
        }
        public static string Statistics(int nbr)
        {
            string valueString = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
               
                // Create a MySqlCommand object
                using (MySqlCommand command = connection.CreateCommand())
                {
                    string requete = "";
                    switch (nbr)
                    {
                        case 0:
                            requete += "SELECT COUNT(purchase_order.id) FROM client JOIN purchase_order ON client.id = purchase_order.client_id WHERE EXTRACT(MONTH FROM purchase_order.order_date) = EXTRACT(MONTH FROM NOW())  AND EXTRACT(YEAR FROM purchase_order.order_date) = EXTRACT(YEAR FROM NOW())  GROUP BY client.id, client.first_name, client.name ORDER BY COUNT(purchase_order.id) DESC LIMIT 1;";
                            break;
                        case 1:
                            requete += "SELECT COUNT(purchase_order.id), client.id, client.first_name, client.name FROM client JOIN purchase_order ON client.id = purchase_order.client_id WHERE EXTRACT(YEAR FROM purchase_order.order_date) = EXTRACT(YEAR FROM NOW()) GROUP BY client.id, client.first_name, client.name ORDER BY COUNT(purchase_order.id) DESC LIMIT 10;";

                            break;
                        case 2:
                            requete += "SELECT AVG(price) AS PrixMoyen FROM(    SELECT price FROM standard_bouquet JOIN purchase_order    ON purchase_order.bouquet_name = standard_bouquet.name    UNION ALL    SELECT price FROM flower_arrangement    ) AS Bouquets;";
                            break;
                        case 3:
                            requete += "SELECT COUNT(purchase_order.id),standard_bouquet.name FROM standard_bouquet JOIN purchase_order ON standard_bouquet.name = purchase_order.bouquet_name GROUP BY standard_bouquet.name ORDER BY COUNT(purchase_order.id) DESC LIMIT 1; ";
                            break;
                        case 4:
                            requete += "SELECT shop.address,SUM(standard_bouquet.price) FROM shop JOIN standard_bouquet JOIN purchase_order ON shop.id=purchase_order.shop_id AND purchase_order.bouquet_name=standard_bouquet.name GROUP BY shop.address ORDER BY SUM(standard_bouquet.price) DESC LIMIT 1;";
                            break;
                        case 5:
                            requete += "SELECT item_name, sum(quantity) FROM arrangement_contains JOIN item ON arrangement_contains.item_name=item.name WHERE item.type='f' GROUP BY item_name ORDER BY SUM(quantity) LIMIT 10;";
                            break;
                        default:
                            // code block
                            break;
                    }
                    command.CommandText = requete;
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    valueString+= Convert.ToString(reader.FieldCount);
                }
            }
            return valueString;

        }
    }
}

using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using UtilityLibrary;
using MySqlX.XDevAPI.Common;
using System.Xml;
using System.Linq.Expressions;
using Newtonsoft.Json;


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
        public static void ExportInactiveClientsToJson()
        {
            // SQL query to retrieve inactive clients
            string sqlQuery = @"
                SELECT c.id, c.first_name, c.name, c.email, c.phone_number, c.address, c.credit_card, c.inscription_date
                FROM client c
                WHERE c.id NOT IN (
                    SELECT p.client_id
                    FROM purchase_order p
                    WHERE p.order_date >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
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

                    // Convert the DataTable to a list of dictionaries
                    List<Dictionary<string, object>> clientData = dataTable.AsEnumerable()
                        .Select(row => dataTable.Columns.Cast<DataColumn>()
                            .ToDictionary(column => column.ColumnName, column => row[column]))
                        .ToList();

                    // Serialize the client data to JSON
                    string jsonData = JsonConvert.SerializeObject(clientData, Newtonsoft.Json.Formatting.Indented);

                    // Save the JSON data to a file or perform any desired operations
                    File.WriteAllText("inactive_clients.json", jsonData);
                }
            }
        }
        public static string GetStatistics(int nbr)
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
                            // Prix moyen de toutes les commandes
                            requete = "SELECT AVG(price) AS PrixMoyen\r\nFROM\r\n(\r\n   SELECT price FROM standard_bouquet JOIN purchase_order\r\n   ON purchase_order.bouquet_name = standard_bouquet.name\r\n   UNION ALL\r\n   SELECT price FROM flower_arrangement\r\n) AS Bouquets;";
                            break;
                        case 1:
                            // Client du mois
                            requete = "SELECT COUNT(purchase_order.id), client.id, client.first_name, client.name FROM client JOIN purchase_order ON client.id = purchase_order.client_id WHERE EXTRACT(YEAR FROM purchase_order.order_date) = EXTRACT(YEAR FROM NOW()) GROUP BY client.id, client.first_name, client.name ORDER BY COUNT(purchase_order.id) DESC LIMIT 10;";

                            break;
                        case 2:
                            // Client de l'année
                            requete = "SELECT COUNT(purchase_order.id), client.id, client.first_name, client.name\r\nFROM client\r\nJOIN purchase_order ON client.id = purchase_order.client_id\r\nWHERE EXTRACT(YEAR FROM purchase_order.order_date) = EXTRACT(YEAR FROM NOW())\r\nGROUP BY client.id, client.first_name, client.name\r\nORDER BY COUNT(purchase_order.id) DESC\r\nLIMIT 10;";
                            break;
                        case 3:
                            // Bouquet standard le plus acheté
                            requete = "SELECT COUNT(purchase_order.id),standard_bouquet.name\r\nFROM standard_bouquet\r\nJOIN purchase_order ON standard_bouquet.name = purchase_order.bouquet_name\r\nGROUP BY standard_bouquet.name\r\nORDER BY COUNT(purchase_order.id) DESC\r\nLIMIT 10;";
                            break;
                        case 4:
                            // Magasin avec le plus gros chiffre d'affaire
                            requete = "SELECT shop.address,SUM(standard_bouquet.price)\r\nFROM shop JOIN standard_bouquet JOIN purchase_order\r\nON shop.id=purchase_order.shop_id AND purchase_order.bouquet_name=standard_bouquet.name\r\nGROUP BY shop.address\r\nORDER BY SUM(standard_bouquet.price) DESC\r\nLIMIT 10;";
                            break;
                        case 5:
                            // Fleur la moins vendure
                            requete = "SELECT item_name, sum(quantity)\r\nFROM arrangement_contains JOIN item\r\nON arrangement_contains.item_name=item.name\r\nWHERE item.type='f'\r\nGROUP BY item_name\r\nORDER BY SUM(quantity)\r\nLIMIT 10;";
                            break;
                        default:
                            // code block
                            break;
                    }
                    command.CommandText = requete;
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    reader.Read();
                    switch (nbr)
                    {
                        case 1:
                        case 2:
                            // Prix moyen de toutes les commandes
                            valueString += reader.GetString(2) + " " + reader.GetString(3) + " avec " + Convert.ToString(reader.GetInt32(0)) + " achats";
                            break;
                        case 3:
                            valueString += reader.GetString(1) + " avec " + Convert.ToString(reader.GetInt32(0))  + " ventes";
                            break;
                        case 4:
                            valueString += reader.GetString(0) + " avec " + Convert.ToString(reader.GetInt32(1)) + "€";
                            break;
                        case 5:
                            valueString += reader.GetString(0) + " avec " + Convert.ToString(reader.GetInt32(1)) + " ventes";
                            break;
                        default:
                            
                            valueString += Convert.ToString(reader.GetFloat(0));
                            break;
                    }
                }
            }
            return valueString;

        }
    }
}
